using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Library;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jukebox.Controllers
{
    public class MusicPlayerController : WebApiController
    {
        //private MediaServer _mediaServer;

        public MusicPlayerController()
        {
            /*
            var instance = MediaServer.GetInstance();
            instance.Wait();
            _mediaServer = instance.Result;
            */
        }

        [Route(HttpVerbs.Get, "/")]
        public async Task<string> NowPlaying()
        {
            return "";
        }

        [Route(HttpVerbs.Get, "/play")]
        public string PlayMusic()
        {
            JukeboxMediaManager.GetInstance().Play();
            
            return "";
        }

        [Route(HttpVerbs.Get, "/play/{id}")]
        public string PlayMusic(int id)
        {
            JukeboxMediaManager.GetInstance().Play();

            return "";
        }

        [Route(HttpVerbs.Get, "/pause")]
        public string PauseMusic()
        {
            JukeboxMediaManager.GetInstance().Pause();
            return "";
        }

        [Route(HttpVerbs.Get, "/next")]
        public async Task<string> NextTrack()
        {
            return "";
        }

        [Route(HttpVerbs.Get, "/prev")]
        public async Task<string> PreviousTrack()
        {
            return "";
        }
    }
}
