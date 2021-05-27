using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using Jukebox.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox.Controllers
{
    public class MediaManagerController : WebApiController
    {
        protected JukeboxDb _db;
        public MediaManagerController() : base()
        {
            _db = JukeboxDb.GetInstance();
        }

        [Route(HttpVerbs.Get, "/")]
        public async Task<IEnumerable<MusicFile>> GetMusic(int limit = 100, int offset = 0)
        {
            var data = await _db.MusicFiles.All(limit, offset);
            return data;
        }

        [Route(HttpVerbs.Get, "/playlist/new")]
        public async Task<string> NewPlaylist()
        {
            var json = JsonConvert.SerializeObject("", Formatting.Indented);
            return json;
        }

        [Route(HttpVerbs.Get, "/search/{search}")]
        public async Task<IEnumerable<MusicFile>> SearchMusic(string search, int limit = 100, int offset = 0) {
            var data = await _db.MusicFiles.Search(search, limit, offset);
            return data;
        }
    }
}
