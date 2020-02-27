using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<string> PlayMusic()
        {
            //await _mediaServer.Play();
            return "";
        }

        [Route(HttpVerbs.Get, "/pause")]
        public async Task<string> PauseMusic()
        {
            //await _mediaServer.Pause();
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
