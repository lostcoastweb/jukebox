using Jukebox.Database;
using Jukebox.Library;
using Jukebox.Models;
using MediaManager;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using StandardStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jukebox.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private Dictionary<string, string> _musicFileExtensions = new Dictionary<string, string>();
        
        public ICommand PlayMusicCommand { get; set; }
        public ICommand PickMusicPathCommand { get; set; }
        public ICommand ScanForMusicCommand { get; set; }
        public ICommand PauseMusicCommand { get; set; }
        public MainPageViewModel() : base()
        {
            _musicFileExtensions.Add(".mp3", ".mp3");

            PlayMusicCommand = new Command(async () => { await PlayMusic(); });
            PauseMusicCommand = new Command(async () => { await PauseMusic(); });
            PickMusicPathCommand = new Command(async () => { await PickMusicPath(); });
            ScanForMusicCommand = new Command(async () => { await ScanForMusic(); });
        }

        private async Task<List<MusicFile>> GetFiles(string path)
        {
            List<MusicFile> files = new List<MusicFile>();
            await GetFiles(path, files);
            return files;
        }

        private async Task GetFiles(string path, List<MusicFile> musicFiles)
        {
            await Task.Run(async () => {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (_musicFileExtensions.ContainsKey(Path.GetExtension(file)) == true)
                    {
                        var tfile = TagLib.File.Create(file);

                        MusicFile musicFile = new MusicFile() {
                            Album = tfile.Tag.Album,
                            Artist = tfile.Tag.FirstAlbumArtist,
                            Path = file,
                            Title = tfile.Tag.Title,
                            TrackNumber = (int)tfile.Tag.Track,
                            Year = (int)tfile.Tag.Year
                            
                        };
                        musicFiles.Add(musicFile);
                    }
                }
                var directories = Directory.GetDirectories(path);
                foreach(var directory in directories)
                {
                    if (Directory.Exists(directory))
                    {
                        //recurse if directory
                        await GetFiles(directory, musicFiles);
                    }
                }
            });
        }

        private async Task ScanForMusic()
        {
            List<MusicFile> musicFiles = new List<MusicFile>();
            System.Environment.SpecialFolder[] defaultMusicPaths = { System.Environment.SpecialFolder.CommonMusic, System.Environment.SpecialFolder.MyMusic};
            foreach(var folder in defaultMusicPaths)
            {
                var path = System.Environment.GetFolderPath(folder);
                var files = await GetFiles(path);
                musicFiles.AddRange(files);
            }
            JukeboxDb db = JukeboxDb.GetInstance();
            await db.MusicFiles.Clear();
            var numInserted = await db.MusicFiles.Add(musicFiles);

        }

        private async Task PauseMusic()
        {
            await CrossMediaManager.Current.Pause();
        }

        private async Task PlayMusic()
        {
            if (CrossMediaManager.Current.Duration > new TimeSpan())
            {
                await CrossMediaManager.Current.Play();
            }
            else
            {
                await CrossMediaManager.Current.Play("http://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            }
            //var musicPlayer = await MediaServer.GetInstance();
            //await musicPlayer.Play();
        }

        private async Task PlayMusic(string path)
        {
            var musicPlayer = Library.JukeboxMediaManager.GetInstance();
            musicPlayer.Play();
        }

        private async Task PickMusicPath()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                { 
                    return; // user canceled file picking
                }

                string fileName = fileData.FileName;
                string selectedPath = Path.GetDirectoryName(fileData.FilePath);
                var files = Directory.GetFiles(selectedPath);
                if(files.Length > 0)
                {
                    await PlayMusic(files[0]);
                }
                //var files = await FileSystem.Current.LocalStorage.GetFilesAsync(fileData.FilePath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
    }
}
