using System;
using System.Collections.Generic;
using System.Text;

namespace Jukebox.Models
{
    public class MusicFile
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Track_Number { get; set; }
    }
}
