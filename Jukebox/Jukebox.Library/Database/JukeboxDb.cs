using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Jukebox.Library.Database
{
    public class JukeboxDb
    {
        public ConfigDb Config { get; private set; }

        public JukeboxDb()
        {
            Config = new ConfigDb(JukeboxDbConnection);
        }

        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\jukebox.db"; }
        }

        public static SQLiteConnection JukeboxDbConnection
        {
            get
            {
                return new SQLiteConnection("Data Source=" + DbFile);
            }
        }
    }
}
