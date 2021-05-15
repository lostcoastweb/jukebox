using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jukebox.Models
{
    public class Playlist
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public List<MusicFile> Songs { get; set; }
        public int CurrentSongIndex { get; set; }
        public MusicFile CurrentSong
        {
            get
            {
                return Songs[CurrentSongIndex];
            }
        }
       public Playlist()
        {
            Name = "";
            Songs = new List<MusicFile> { };
            CurrentSongIndex = 0;
        }

        //Converts playlist into an ilist for xamarin media manager to use
        public IList<string> AllMusicFilePath()
        {
            return Songs.Select(s => s.Path).ToList();
        }
    }
}
