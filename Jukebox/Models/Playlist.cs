using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;


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

            IList<string> relativePaths = Songs.Select(s => s.Path).ToList();
            IList<string> absolutePaths =  new List<string>();
            for(int i =0; i<relativePaths.Count();i++)
            {
                // Open an existing file, or create a new one.
                FileInfo fi = new FileInfo(relativePaths[i]);
                // Determine the full path of the file just created.
                DirectoryInfo di = fi.Directory;

                var result = Path.Combine(di.ToString(), relativePaths[i].Remove(0, 28));
                absolutePaths.Add(result.ToString());
            }
            for (int i = 0; i < absolutePaths.Count(); i++)
                Trace.WriteLine(absolutePaths[i]);


            return absolutePaths;
        }
    }
}
