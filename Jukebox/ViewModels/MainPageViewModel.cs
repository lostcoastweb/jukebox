using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jukebox.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand PlayMusicCommand { get; set; }
        public MainPageViewModel() : base()
        {
            PlayMusicCommand = new Command(async () => { await PlayMusic(); });
        }

        private async Task PlayMusic()
        {
            await CrossMediaManager.Current.Play("http://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
        }
    }
}
