using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Jukebox.Database
{
    public class PlaylistDb
    {
        private DbConnection _db;
        public PlaylistDb(DbConnection db)
        {
            _db = db;
        }
    }
}
