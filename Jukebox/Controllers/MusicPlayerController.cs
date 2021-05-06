using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using Jukebox.Library;
using Jukebox.Models;
using MediaManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jukebox.Controllers
{
    public class MusicPlayerController : WebApiController
    {
        protected JukeboxDb _db;

      

        public MusicPlayerController()
        {
            _db = JukeboxDb.GetInstance();
        }

        [Route(HttpVerbs.Get, "/")]
        public async Task<string> NowPlaying()
        {
            return "";
        }

        [Route(HttpVerbs.Get, "/files")]
        public async Task<IEnumerable<MusicFile>> GetMusic(int limit = 100, int offset = 0)
        {
            var data = await _db.MusicFiles.All(limit, offset);
            return data;
        }

        [Route(HttpVerbs.Get, "/play")]
        public string PlayMusic()
        {
            JukeboxMediaManager.GetInstance().Play();
            
            return "play";
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
            JukeboxMediaManager.GetInstance().PlayNext();
            return "";
        }

        [Route(HttpVerbs.Get, "/prev")]
        public async Task<string> PreviousTrack()
        {
            JukeboxMediaManager.GetInstance().PlayPrev();
            return "";
        }

        [Route(HttpVerbs.Get, "/volDown")]
        public async Task<string> VolumeDown()
        {
            JukeboxMediaManager.GetInstance().VolumeDown();
            return "";
        }

        [Route(HttpVerbs.Get, "/volUp")]
        public async Task<string> VolumeUp()
        {
            JukeboxMediaManager.GetInstance().VolumeUp();
            return "";
        }

        [Route(HttpVerbs.Get, "/mute")]
        public async Task<string> Mute()
        {
            JukeboxMediaManager.GetInstance().Mute();
            return "";
        }
    }
}
