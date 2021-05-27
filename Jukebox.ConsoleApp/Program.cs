using Jukebox.Library;
using MediaManager;
using System;
using System.Threading.Tasks;

namespace Jukebox.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CrossMediaManager.Current.Init();
            MediaServer mediaServer = new MediaServer();
            mediaServer.PlayFile();
            JukeboxServer server = new JukeboxServer();
            await server.Start();
            Console.WriteLine("Done.");
        }
    }
}
