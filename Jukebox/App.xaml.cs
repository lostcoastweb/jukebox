using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using System.Diagnostics;
using EmbedIO;
using EmbedIO.WebApi;
using EmbedIO.Routing;

namespace Jukebox
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Server must be started, before WebView is initialized,
            // because we have no reload implemented in this sample.
            Task.Factory.StartNew(async () =>
            {
                using (var server = new WebServer("http://*:8080"))
                {
                    //Assembly assembly = typeof(App).Assembly;
                    server.WithLocalSessionManager();
                    server.WithWebApi("/api", m => m.WithController(() => new TestController()));
                    //server.WithEmbeddedResources("/", assembly, "EmbedIO.Forms.Sample.html");
                    await server.RunAsync();
                }
            });
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

        [Route(HttpVerb.Get, "/testresponse")]
        public int GetTestResponse()
        {
            return 12345;
        }
    }

}
