using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Diagnostics;

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

        public async Task<IEnumerable<MusicFile>> All(int limit, int offset = 0)
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
                           FROM music_files 
                           LIMIT @limit
                           OFFSET @offset";
            var result = await _db.QueryAsync<MusicFile>(sql, new { limit = limit, offset = offset });
            var items = result.ToList();
            _db.Close();
            return items;
        }

        public async Task<IEnumerable<MusicFile>> All(string search, int limit, int offset = 0)
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
                           FROM music_files 
                           WHERE instr(title, @search)>0
                           OR instr(artist, @search)>0
                           OR instr(album, @search)>0
                           LIMIT @limit
                           OFFSET @offset";
            try
            {
                var result = await _db.QueryAsync<MusicFile>(sql, new { limit = limit, offset = offset, search = search.ToLower() });
                var items = result.ToList();
                _db.Close();
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception encountered: " + e.Message);
                _db.Close();
                return null;
            }
        }

        public async Task<int> Count()
        {
            _db.Open();
            string sql = @"SELECT COUNT(id) FROM music_files";
            var count = await _db.ExecuteScalarAsync(sql);
            _db.Close();
            int count_int = -1;
            Int32.TryParse(count.ToString(), out count_int);
            return count_int;
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
            var affectedRows = 0;
            try
            {
                affectedRows = await _db.ExecuteAsync(sql,
                new { path = file.Path, album = file.Album, artist = file.Artist, title = file.Title, year = file.Year, track_number = file.TrackNumber });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _db.Close();
            return affectedRows == 1;
        }

        public async Task<int> Add(IList<MusicFile> files)
        {
            if (files.Count < 1)
            {
                return 0;
            }

            _db.Open();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO music_files (
                            path,
                            album,
                            artist,
                            title,
                            year,
                            track_number
                        )
                        VALUES ");
            int counter = 0;
            DynamicParameters parameters = new DynamicParameters();
            
            foreach (MusicFile file in files)
            {
                Dictionary<string, string> values = new Dictionary<string, string>() {
                    { string.Format("path{0}", counter), file.Path },
                    { string.Format("album{0}", counter), file.Album },
                    { string.Format("artist{0}", counter), file.Artist },
                    { string.Format("title{0}", counter), file.Title },
                    { string.Format("year{0}", counter), file.Year.ToString() },
                    { string.Format("track_number{0}", counter), file.TrackNumber.ToString() }
                };

                sql.Append("(");
                foreach(var kvp in values)
                {
                    sql.Append(string.Format("@{0}, ", kvp.Key));
                    parameters.Add(kvp.Key, kvp.Value);
                }

                //remove last comma
                sql.Remove(sql.Length - 2, 1);
                sql.Append("), ");

                //increment insert counter
                counter++;
            }

            //remove last comma
            sql.Remove(sql.Length - 2, 1);
            int affectedRows = -1;
            try
            {
                affectedRows = await _db.ExecuteAsync(sql.ToString(), parameters);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _db.Close();
            return affectedRows;
        }
    }
}
