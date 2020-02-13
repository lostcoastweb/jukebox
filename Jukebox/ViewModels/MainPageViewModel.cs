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
        public ICommand PlayMusicCommand { get; set; }
        public ICommand PickMusicPathCommand { get; set; }
        public MainPageViewModel() : base()
        {
            PlayMusicCommand = new Command(async () => { await PlayMusic(); });
            PickMusicPathCommand = new Command(async () => { await PickMusicPath(); });
        }

        private async Task PlayMusic()
        {
            await CrossMediaManager.Current.Play("http://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
        }

        private async Task PickMusicPath()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                string fileName = fileData.FileName;
                string selectedPath = Path.GetDirectoryName(fileData.FilePath);
                var files = Directory.GetFiles(selectedPath);
                //var files = await FileSystem.Current.LocalStorage.GetFilesAsync(fileData.FilePath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
    }
}
