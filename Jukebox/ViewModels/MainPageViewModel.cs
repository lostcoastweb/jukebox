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
        public MediaManagerViewModel MediaManager {get;set;}
        public ICommand PlayMusicCommand { get; set; }
        public ICommand PickMusicPathCommand { get; set; }
        public ICommand PauseMusicCommand { get; set; }
        public MainPageViewModel() : base()
        {
            MediaManager = new MediaManagerViewModel();
            PlayMusicCommand = new Command(async () => { await PlayMusic(); });
            PauseMusicCommand = new Command(async () => { await PauseMusic(); });
            PickMusicPathCommand = new Command(async () => { await PickMusicPath(); });
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
