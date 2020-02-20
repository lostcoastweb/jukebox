using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Jukebox.Database
{
    public class MusicFilesDb
    {
        private DbConnection _db;
        public MusicFilesDb(DbConnection db)
        {
            _db = db;
        }


        public async Task<IEnumerable<MusicFile>> All()
        {
            _db.Open();
            string sql = @"SELECT 
                           id As Id,
                            path as Path,
                            album as Album,
                            artist AS Artist,
                            title AS Title,
                            year AS Year,
                            track_number AS TrackNumber
                           FROM music_files";
            var result = await _db.QueryAsync<MusicFile>(sql);
            var items = result.ToList();
            _db.Close();
            return items;
        }

        public async Task<int> Count()
        {
            _db.Open();
            string sql = @"SELECT COUNT(id) FROM music_files";
            var count = await _db.ExecuteAsync(sql);
            _db.Close();
            return count;
        }

        public async Task<int> Clear()
        {
            
            int before = await Count();
            _db.Open();
            string sql = "DELETE FROM music_files";
            var query = await _db.ExecuteAsync(sql);
            _db.Close();
            int after = await Count();
            return before - after;
        }

        public async Task<bool> Add(MusicFile file)
        {
            _db.Open();
            string sql = @"INSERT INTO music_files (
                            path,
                            album,
                            artist,
                            title,
                            year,
                            track_number
                        )
                        VALUES (
                            @path,
                            @album,
                            @artist,
                            @title,
                            @year,
                            @track_number
                        );";
            var affectedRows = await _db.ExecuteAsync(sql,
                new { path = file.Path, album = file.Album, artist = file.Artist, title = file.Title, year = file.Year, track_number = file.TrackNumber });
            _db.Close();
            return affectedRows == 1;
        }
    }
}
