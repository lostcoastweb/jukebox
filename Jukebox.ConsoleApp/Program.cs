using Jukebox.Library;
using System;
using System.Threading.Tasks;

namespace Jukebox.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            JukeboxServer server = new JukeboxServer();
            await server.Start();
            Console.WriteLine("Done.");
        }
    }
}
