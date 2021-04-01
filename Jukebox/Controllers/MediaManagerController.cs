using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
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
        public async Task<string> GetMusic(int limit = 100, int offset = 0)
        {
            var data = await _db.MusicFiles.All(limit, offset);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            return json;
        }

        public async Task<string> SearchMusic(string search, int limit = 100, int offset = 0) {
            var data = await _db.MusicFiles.All(search, limit, offset);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            return json;
        }
    }
}
