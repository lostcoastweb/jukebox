using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox.Controllers
{
    public class PlaylistController : WebApiController
    {
        protected JukeboxDb _db;
        protected PlaylistDb _pdb;
        public PlaylistController() : base()
        {
            _db = JukeboxDb.GetInstance();
            _pdb = PlaylistDb.GetInstance();
        }

        [Route(HttpVerbs.Get, "/")]
        public async Task<IEnumerable<Playlist>> GetAllPlaylists()
        {
            var music_files = await _pdb.All();
            
            return music_files;
        }

        [Route(HttpVerbs.Post, "/")]
        public async Task<bool> PostPlaylist()
        {
            var rawData = await HttpContext.GetRequestFormDataAsync();
            if (rawData.Get("name") != null)
            {
                Playlist playlist = new Playlist();
                playlist.Name = rawData.Get("name");
                return await _pdb.Add(playlist);
            }

            return false;
        }

        [Route(HttpVerbs.Get, "/{id}")]
        public async Task<Playlist> GetPlaylist(int id)
        {
            Playlist playlist = await _pdb.Select(id);
            return playlist;
        }
    }
}
