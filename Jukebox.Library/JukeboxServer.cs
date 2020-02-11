using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Library.Controllers;
using System;
using System.Threading.Tasks;

namespace Jukebox.Library
{
    public class JukeboxServer
    {
        public async Task<bool> Start()
        {
            using (var server = new WebServer("http://*:8080"))
            {
                //Assembly assembly = typeof(App).Assembly;
                server.WithLocalSessionManager();
                server.WithWebApi("/api/config", m => m.WithController(() => new ConfigController()));
                //server.WithWebApi("/api", m => m.WithController(() => new TestController()));
                //server.WithEmbeddedResources("/", assembly, "EmbedIO.Forms.Sample.html");
                await server.RunAsync();
            }
            return true;
        }
    }
}
