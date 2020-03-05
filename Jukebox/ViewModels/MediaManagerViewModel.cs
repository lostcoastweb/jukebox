using Jukebox.Database;
using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jukebox.ViewModels
{
    public class MediaManagerViewModel : ViewModelBase
    {
        private Dictionary<string, string> _fileExtensions = new Dictionary<string, string>();
        public List<string> FileExtensions
        {
            get
            {
                return _fileExtensions.Keys.ToList();
            }
        }

        public ICommand ScanForMusicCommand { get; set; }

        public MediaManagerViewModel()
        {
            ScanForMusicCommand = new Command(async () => { await ScanForMusic(); });

            //TODO: learn what other media types are supported
            _fileExtensions.Add(".mp3", ".mp3");
        }

        private async Task<List<MusicFile>> GetFiles(string path)
        {
            List<MusicFile> files = new List<MusicFile>();
            await GetFiles(path, files);
            return files;
        }

        private async Task GetFiles(string path, List<MusicFile> musicFiles)
        {
            await Task.Run(async () => {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (_fileExtensions.ContainsKey(Path.GetExtension(file)) == true)
                    {
                        var tfile = TagLib.File.Create(file);

                        MusicFile musicFile = new MusicFile()
                        {
                            Album = tfile.Tag.Album,
                            Artist = tfile.Tag.FirstAlbumArtist,
                            Path = file,
                            Title = tfile.Tag.Title,
                            TrackNumber = (int)tfile.Tag.Track,
                            Year = (int)tfile.Tag.Year

                        };
                        musicFiles.Add(musicFile);
                    }
                }
                var directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                {
                    if (Directory.Exists(directory))
                    {
                        //recurse if directory
                        await GetFiles(directory, musicFiles);
                    }
                }
            });
        }

        /// <summary>
        /// Scans common system paths for music.  Will clear existing library.
        /// </summary>
        /// <returns></returns>
        private async Task ScanForMusic()
        {
            List<MusicFile> musicFiles = new List<MusicFile>();
            System.Environment.SpecialFolder[] defaultMusicPaths = { System.Environment.SpecialFolder.CommonMusic, System.Environment.SpecialFolder.MyMusic };
            foreach (var folder in defaultMusicPaths)
            {
                var path = System.Environment.GetFolderPath(folder);
                var files = await GetFiles(path);
                musicFiles.AddRange(files);
            }
            JukeboxDb db = JukeboxDb.GetInstance();
            await db.MusicFiles.Clear();
            var numInserted = await db.MusicFiles.Add(musicFiles);

        }

    }
}
