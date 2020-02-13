using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Library.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jukebox.Library
{
    public class JukeboxServer
    {
        public void Start()
        {
            Thread server = new Thread(new ThreadStart(RunWebServer));
            server.Start();
        }

        private async void RunWebServer()
        {
            using (var server = new WebServer("http://*:8080"))
            {
                //Assembly assembly = typeof(App).Assembly;
                server.WithLocalSessionManager();
                server.WithWebApi("/api/config", m => m.WithController(() => new ConfigController()));
                server.WithWebApi("/api/fs", m => m.WithController(() => new FileSystemController()));
                //server.WithWebApi("/api", m => m.WithController(() => new TestController()));
                //server.WithEmbeddedResources("/", assembly, "EmbedIO.Forms.Sample.html");
                await server.RunAsync();
            }
        }
    }
}
