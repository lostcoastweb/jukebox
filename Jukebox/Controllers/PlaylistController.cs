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
            if (rawData.Get("name") != null && rawData.Get("songs") != null)
            {
                Playlist playlist = new Playlist();
                playlist.Name = rawData.Get("name");
                List<MusicFile> songs = new List<MusicFile>();
                var ids = rawData.Get("songs").Split(',');
                foreach (string id in ids)
                {
                    int song_id;
                    bool parse = int.TryParse(id, out song_id);
                    if (parse)
                    {
                        var song = new MusicFile();
                        song.Id = song_id;
                        songs.Add(song);
                    }
                    else
                    {
                        throw new Exception("Non integer song ID found in urlencoded data.");
                    }
                }
                playlist.Songs = songs;
                return await _pdb.Add(playlist);
            }

            return false;
        }

        [Route(HttpVerbs.Get, "/{id}")]
        public async Task<Playlist> GetPlaylist(string id)
        {
            int int_id = 0;
            bool success = int.TryParse(id, out int_id);
            if (success)
            {
                Playlist playlist = await _pdb.Select(int_id);
                return playlist;
            }
            else
            {
                return new Playlist();
            }
        }
    }
}
