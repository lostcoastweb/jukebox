using StandardStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Jukebox.Library;
using MediaManager;

namespace Jukebox
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new ViewModel();
            /*
             *
            List<string> folders = new List<string>();
            foreach (Environment.SpecialFolder folder in (Environment.SpecialFolder[])Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                folders.Add(System.Environment.GetFolderPath(folder));
            }
            */
            CrossMediaManager.Current.Play("https://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            //MediaServer mediaServer = new MediaServer();
            //mediaServer.PlayFile();
            JukeboxServer server = new JukeboxServer();
            //server.Start();
            Console.WriteLine("Done.");
        }

        static void PrintFolderPath(System.Environment.SpecialFolder folder) => Console.WriteLine($"{folder}={System.Environment.GetFolderPath(folder)}");
    }

}
