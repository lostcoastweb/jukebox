using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using Jukebox.Library;
using MediaManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Dictionary<string, string> PlayMusic()
        {
            JukeboxMediaManager.GetInstance().Play();

            return JukeboxMediaManager.GetInstance().getCurrentMetadata();
        }

        [Route(HttpVerbs.Get, "/play/{id}")]
        public string PlayMusic(int id)
        {
            JukeboxMediaManager.GetInstance().Play();

            return "";
        }

        [Route(HttpVerbs.Get, "/pause")]
        public Dictionary<string, string> PauseMusic()
        {
            JukeboxMediaManager.GetInstance().Pause();
           

            return JukeboxMediaManager.GetInstance().getCurrentMetadata();
        }

        [Route(HttpVerbs.Get, "/next")]
        public async Task<Dictionary<string, string>>  NextTrack()
        {
            if(CrossMediaManager.Current.IsPlaying())
            {
                await Task.Run(() => JukeboxMediaManager.GetInstance().PlayNext());
            }
            else
            {
                await Task.Run(() => JukeboxMediaManager.GetInstance().PlayNext());
                JukeboxMediaManager.GetInstance().Pause();
            }
            if (CrossMediaManager.Current.Queue.HasNext)
            {
                return JukeboxMediaManager.GetInstance().getNextMetadata();

            }
            else
            {
                return JukeboxMediaManager.GetInstance().getCurrentMetadata();
            };

        }
     
        void playPrevPause()
        {
            JukeboxMediaManager.GetInstance().PlayPrev();
            JukeboxMediaManager.GetInstance().Pause();
           
        }

        void playPrev()
        {
            JukeboxMediaManager.GetInstance().PlayPrev();
            
        }


        [Route(HttpVerbs.Get, "/prev")]
        public async Task<Dictionary<string, string>> PreviousTrack()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                
                await Task.Run(()=>playPrev());
            }
            else
            {
                await Task.Run(() => playPrevPause());
            }


            if (CrossMediaManager.Current.Queue.HasPrevious)
            {
                 
                    return JukeboxMediaManager.GetInstance().getPrevMetadata();          
            }
            else {
                return JukeboxMediaManager.GetInstance().getCurrentMetadata();
            };

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


        [Route(HttpVerbs.Get, "/seek/{seekValue}")]
        public async Task<float> Seek(float seekValue)
        {
            TimeSpan seekTime = TimeSpan.FromSeconds(seekValue);
            var seek = JukeboxMediaManager.GetInstance().Seek(seekTime);
            return seekValue;
        }
    }
}
