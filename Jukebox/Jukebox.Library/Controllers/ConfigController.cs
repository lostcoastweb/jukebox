using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using Jukebox.Library.Models;
using Jukebox.Library.Database;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jukebox.Library.Controllers
{
    public class ConfigController : WebApiController
    {
        protected JukeboxDb _db;
        public ConfigController() : base()
        {
            _db = new JukeboxDb();
        }

        [Route(HttpVerbs.Get, "/music/paths")]
        public async Task<string> GetMusicPaths()
        {
            var result = await _db.Config.GetMusicRoutes();
            return result.Value;
        }

        [Route(HttpVerbs.Post, "/music/paths")]
        public async Task<string> PostMusicPaths()
        {
            var rawData = await HttpContext.GetRequestFormDataAsync();
            return "";
        }


        [Route(HttpVerbs.Post, "/login")]
        public int GetTestResponse()
        {
            return -1;
        }
    }
}
