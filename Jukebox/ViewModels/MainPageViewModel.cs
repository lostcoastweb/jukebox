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
        public MainPageViewModel() : base()
        {
            _musicFileExtensions.Add("mp3", "mp3");

            PlayMusicCommand = new Command(async () => { await PlayMusic(); });
            PickMusicPathCommand = new Command(async () => { await PickMusicPath(); });
            ScanForMusicCommand = new Command(async () => { await ScanForMusic(); });
        }

        private async Task GetFiles(string path)
        {
            List<string> files = new List<string>();
            await GetFiles(path, files);
        }

        private async Task GetFiles(string path, List<string> musicFiles)
        {
            await new Task(async () => {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (Directory.Exists(file))
                    {
                        //recurse if directory
                        await GetFiles(file, musicFiles);
                    }
                    else if (_musicFileExtensions.ContainsKey(Path.GetExtension(file)) == true)
                    {
                        musicFiles.Add(file);
                    }
                }
            });
            
        }

        private async Task ScanForMusic()
        {
            List<string> musicFiles = new List<string>();
            System.Environment.SpecialFolder[] defaultMusicPaths = { System.Environment.SpecialFolder.CommonMusic, System.Environment.SpecialFolder.MyMusic};
            foreach(var folder in defaultMusicPaths)
            {
                var path = System.Environment.GetFolderPath(folder);
            }
        }

        private async Task PlayMusic()
        {
            await CrossMediaManager.Current.Play("http://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
        }

        private async Task PlayMusic(string path)
        {
            await CrossMediaManager.Current.Play(path);
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
