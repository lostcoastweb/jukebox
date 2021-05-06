using Dapper;
using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox.Database
{
    public class PlaylistDb
    {
        private static PlaylistDb _instance;

        private DbConnection _db;
        public PlaylistDb(DbConnection db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Playlist>> All()
        {
            _db.Open();
            string sql = @"SELECT 
                           *
                           FROM playlists";
            var result = await _db.QueryAsync<Playlist>(sql);
            var items = result.ToList();
            _db.Close();
            return items;
        }

        public async Task<Playlist> Select(int id)
        {
            /*_db.Open();
            string sql = @"SELECT p.name, p.id, pm.music_id, mf.album, mf.artist, mf.title FROM playlists p
                            INNER JOIN playlist_music pm ON pm.playlist_id = p.id 
                            INNER JOIN music_files mf ON pm.music_id = mf.id
                            WHERE p.id = @id";

            IEnumerable<Playlist> result = await _db.QueryAsync<Playlist>(sql, new { id = id });
            var item = result.ToList()[0];
            _db.Close();
            return item;*/

            //
            // referenced extensively from: https://dapper-tutorial.net/query#example---query-multi-mapping-one-to-many
            //

            string sql = "SELECT TOP 10 * FROM Orders AS A INNER JOIN OrderDetails AS B ON A.OrderID = B.OrderID;";

            var playlistDictionary = new Dictionary<int, Playlist>();

            var list = _db.Query<Playlist, MusicFile, Playlist>(sql,
            (playlist, music) =>
            {
                Playlist playlistEntry;

                if (!playlistDictionary.TryGetValue(music.Id, out playlistEntry))
                {
                    playlistEntry = playlist;
                    playlistEntry.Songs = new List<MusicFile>();
                    playlistDictionary.Add(playlist.ID, playlistEntry);
                }

                playlistEntry.Songs.Add(music);
                return playlistEntry;
            },
            splitOn: "id")
            .Distinct()
            .ToList();

            Console.WriteLine(list.Count);
        }

        public async Task<bool> Add(Playlist playlist)
        {
            _db.Open();
            string sql = @"INSERT INTO playlists (
                            name,
                            date_created,
                            last_modified
                        )
                        VALUES (
                            @name,
                            @date,
                            @modified
                        );";
            var affectedRows = 0;
            try
            {
                string datetime = DateTime.Now.ToString("%Y-%M-%D %HH:%mm:%ss");
                affectedRows = await _db.ExecuteAsync(sql,
                new { name = playlist.Name, date_created = datetime, last_modified = datetime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _db.Close();
            return affectedRows == 1;
        }

        public static PlaylistDb GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PlaylistDb(PlaylistDbConnection);
            }

            return _instance;
        }
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\jukebox.db"; }
        }

        public static SQLiteConnection PlaylistDbConnection
        {
            get
            {
                return new SQLiteConnection("Data Source=" + DbFile);
            }
        }
    }
}
