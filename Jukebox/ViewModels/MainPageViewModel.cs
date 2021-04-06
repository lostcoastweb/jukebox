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
        public MediaManagerViewModel MediaManager { get; set; }
        public ICommand PlayMusicCommand { get; set; }
        public ICommand PickMusicPathCommand { get; set; }
        public ICommand PauseMusicCommand { get; set; }
        public ICommand NextSongCommand { get; set; }
        public ICommand PrevSongCommand { get; set; }

        public IList<string> myPlaylist => new[]{
            "C:/Users/dbjer/Music/playlist/track1.mp3",
            "C:/Users/dbjer/Music/playlist/track2.mp3",
            "C:/Users/dbjer/Music/playlist/track3.mp3",
            "C:/Users/dbjer/Music/playlist/track4.mp3",
        };

        public MainPageViewModel() : base()
        {
            MediaManager = new MediaManagerViewModel();
            PlayMusicCommand = new Command(async () => { await PlayMusic(); });
            PauseMusicCommand = new Command(async () => { await PauseMusic(); });
            NextSongCommand = new Command(async () => { await NextSong(); });
            PrevSongCommand = new Command(async () => { await PrevSong(); });
            PickMusicPathCommand = new Command(async () => { await PickMusicPath(); });

        }

        private async Task NextSong()
        {
            await CrossMediaManager.Current.PlayNext();
        }
        private async Task PrevSong()
        {
            await CrossMediaManager.Current.PlayPreviousOrSeekToStart();
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
                await CrossMediaManager.Current.Play(myPlaylist);
            }
            //http://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3
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
