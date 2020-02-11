using EmbedIO;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using Jukebox.Library.Models;
using Jukebox.Library.Database;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EmbedIO.Routing;

namespace Jukebox.Library.Controllers
{
    public class ConfigController : WebApiController
    {
        protected JukeboxDb _db;
        public ConfigController() : base()
        {
            _db = new JukeboxDb();
        }

        [Route(HttpVerb.Get, "/music/paths")]
        public async Task<string> GetMusicPaths()
        {
            var result = await _db.Config.GetMusicRoutes();
            return result.Value;
        }

        [Route(HttpVerb.Post, "/music/paths")]
        public async Task<string> PostMusicPaths()
        {
            var rawData = await HttpContext.GetRequestFormDataAsync();
            return "";
        }


        [Route(HttpVerb.Post, "/login")]
        public int GetTestResponse()
        {
            return -1;
        }
    }
}
