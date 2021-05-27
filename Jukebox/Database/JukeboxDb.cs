using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Jukebox.Database
{
    public class JukeboxDb
    {
        private static JukeboxDb _instance;

        public ConfigDb Config { get; private set; }
        public MusicFilesDb MusicFiles { get; private set; }

        private JukeboxDb()
        {
            Config = new ConfigDb(JukeboxDbConnection);
            MusicFiles = new MusicFilesDb(JukeboxDbConnection);
        }

        public static JukeboxDb GetInstance()
        {
            if(_instance == null)
            {
                _instance = new JukeboxDb();
            }
            return _instance;
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
