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
using Jukebox.ViewModels;
using Jukebox.Models;
using Jukebox.Views;

namespace Jukebox
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
            MasterBehavior = MasterBehavior.Popover;
            

            // MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
            /*
             *
            List<string> folders = new List<string>();
            foreach (Environment.SpecialFolder folder in (Environment.SpecialFolder[])Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                folders.Add(System.Environment.GetFolderPath(folder));
            }
            */
            //CrossMediaManager.Current.Play("http://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            //MediaServer mediaServer = new MediaServer();
            //mediaServer.PlayFile();
            JukeboxWebServer server = new JukeboxWebServer();
            server.Start();
            Console.WriteLine("Done.");
        }

        static void PrintFolderPath(System.Environment.SpecialFolder folder) => Console.WriteLine($"{folder}={System.Environment.GetFolderPath(folder)}");

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }

}
