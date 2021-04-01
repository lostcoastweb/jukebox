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
            string sql = @"SELECT
                           id As Id,
                           path as Path,
                           album as Album,
                           artist AS Artist,
                           title AS Title,
                           year AS Year,
                           track_number AS TrackNumber
                           FROM music_files
                           WHERE title like " + search
                         +"OR WHERE artist like " + search
                         +"OR WHERE album like " + search;

            var data = await _db.MusicFiles.All(sql, limit, offset);
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
