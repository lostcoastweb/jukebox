using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using System.Diagnostics;
using EmbedIO;
using EmbedIO.WebApi;
using EmbedIO.Routing;
using MediaManager;
using Jukebox.Library;

namespace Jukebox
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            //initialize media player
            CrossMediaManager.Current.Init();
            //var player = MediaServer.GetInstance(CrossMediaManager.Current).Result;

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public class TestController : WebApiController
    {
        public TestController() : base()
        { }

        [Route(HttpVerbs.Get, "/testresponse")]
        public int GetTestResponse()
        {
            return 12345;
        }
    }

}
