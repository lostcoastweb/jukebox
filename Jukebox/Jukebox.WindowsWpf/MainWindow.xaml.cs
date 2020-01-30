using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jukebox.WindowsWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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
