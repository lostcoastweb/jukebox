using System;
using System.Collections.Generic;
using System.Text;

namespace Jukebox.Models
{
    public class Playlist
    {
        public string Name { get; set; }
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
    }
}
