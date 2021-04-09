using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using Jukebox.Library;
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
        public async Task<string> GetMusic(int limit = 100, int offset = 0)
        {
            var data = await _db.MusicFiles.All(limit, offset);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            return json;
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
    }
}
