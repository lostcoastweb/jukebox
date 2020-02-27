using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox.Controllers
{
    public class PlaylistController : WebApiController
    {
        protected JukeboxDb _db;
        public PlaylistController() : base()
        {
            _db = JukeboxDb.GetInstance();
        }

        [Route(HttpVerbs.Get, "/")]
        public async Task<string> GetAllPlaylists()
        {
            return "";
        }

        [Route(HttpVerbs.Post, "/")]
        public async Task<string> PostPlaylist()
        {
            var rawData = await HttpContext.GetRequestFormDataAsync();
            return "";
        }

        [Route(HttpVerbs.Get, "/{id}")]
        public async Task<string> GetPlaylist(int id)
        {
            return "";
        }
    }
}
