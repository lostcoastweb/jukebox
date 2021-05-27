using Dapper;
using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox.Database
{
    public class ConfigDb
    {
        private DbConnection _db;
        public ConfigDb(DbConnection db)
        {
            _db = db;
        }


        public async Task<Config> GetMusicRoutes()
        {
            _db.Open();
            Config result = await _db.QueryFirstAsync<Config>(
            @"SELECT * FROM config
                WHERE key = 'music_routes'");
            _db.Close();
            return result;
        }
    }

}
