using Jukebox.Models;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Jukebox.ViewModels;
using Newtonsoft.Json;


namespace Jukebox.Library
{
    class JukeboxMediaManager
    {
        private bool ServerIsRunning = false;
        private readonly object ServerIsRunningLock = new object();

        private bool IsAudioPlaying = false;
        private readonly object IsAudioPlayingLock = new object();

        private Playlist _currentPlaylist = new Playlist();
        private static JukeboxMediaManager _instance;

        //protected Playlist activePlaylist = new Playlist();

        private JukeboxMediaManager()
        {
            _currentPlaylist.Songs.Add(new MusicFile
            {
                Id = 1,
                Year = 2000,
                TrackNumber = 1,
                Album = "album1",
                Title = "song1",
                Artist = "artist1",
                Path = "C:/Users/dbjer/Music/playlist/track1.mp3",
                Duration= TagLib.File.Create("C:/Users/dbjer/Music/playlist/track1.mp3").Properties.Duration
        });
            _currentPlaylist.Songs.Add(new MusicFile
            {
                Id = 2,
                Year = 2000,
                TrackNumber = 2,
                Album = "album2",
                Title = "song2",
                Artist = "artist2",
                Path = "C:/Users/dbjer/Music/playlist/track2.mp3",
                Duration = TagLib.File.Create("C:/Users/dbjer/Music/playlist/track2.mp3").Properties.Duration

            });
            _currentPlaylist.Songs.Add(new MusicFile
            {
                Id = 3,
                Year = 2000,
                TrackNumber = 3,
                Album = "album3",
                Title = "song3",
                Artist = "artist3",
                Path = "C:/Users/dbjer/Music/playlist/track3.mp3",
                Duration = TagLib.File.Create("C:/Users/dbjer/Music/playlist/track3.mp3").Properties.Duration

            });

        }

        public static JukeboxMediaManager GetInstance(IMediaManager musicContext = null)
        {
            if(_instance == null)
            {
                _instance = new JukeboxMediaManager();
                Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    CrossMediaManager.Current.MediaItemFinished += Current_MediaItemFinished;
                    CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
                });
            }
            return _instance;
        }

        private static void Current_MediaItemFailed(object sender, global::MediaManager.Media.MediaItemFailedEventArgs e)
        {
        }

        private static void Current_MediaItemFinished(object sender, global::MediaManager.Media.MediaItemEventArgs e)
        {
        }

        public Dictionary<string, string> getCurrentMetadata()
        {
            var metaData = new Dictionary<string, string>();
            //get current song index in the playlist
            var index = CrossMediaManager.Current.Queue.CurrentIndex;
            

                metaData.Add("Title", _currentPlaylist.Songs[index].Title);
                metaData.Add("Artist", _currentPlaylist.Songs[index].Artist);
                metaData.Add("Album", _currentPlaylist.Songs[index].Album);
                metaData.Add("Duration", _currentPlaylist.Songs[index].Duration.ToString());
                metaData.Add("durationSeconds", _currentPlaylist.Songs[index].Duration.Seconds.ToString());
                metaData.Add("durationMinutes", _currentPlaylist.Songs[index].Duration.Minutes.ToString());



            return metaData;
        }

        public Dictionary<string, string> getNextMetadata()
        {
            var metaData = new Dictionary<string, string>();
            //get current song index in the playlist
            var index = CrossMediaManager.Current.Queue.CurrentIndex;


            metaData.Add("Title", _currentPlaylist.Songs[index+1].Title);
            metaData.Add("Artist", _currentPlaylist.Songs[index+1].Artist);
            metaData.Add("Album", _currentPlaylist.Songs[index+1].Album);
            metaData.Add("durationSeconds", _currentPlaylist.Songs[index+1].Duration.Seconds.ToString());
            metaData.Add("durationMinutes", _currentPlaylist.Songs[index+1].Duration.Minutes.ToString());



            return metaData;
        }

        public Dictionary<string, string> getPrevMetadata()
        {
            var metaData = new Dictionary<string, string>();
            //get current song index in the playlist
            var index = CrossMediaManager.Current.Queue.CurrentIndex;


            metaData.Add("Title", _currentPlaylist.Songs[index-1].Title);
            metaData.Add("Artist", _currentPlaylist.Songs[index-1].Artist);
            metaData.Add("Album", _currentPlaylist.Songs[index-1].Album);
            metaData.Add("durationSeconds", _currentPlaylist.Songs[index-1].Duration.Seconds.ToString());
            metaData.Add("durationMinutes", _currentPlaylist.Songs[index-1].Duration.Minutes.ToString());



            return metaData;
        }


        public void Play()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await CrossMediaManager.Current.Play();
            });
        }

        public void Play(int index)
        {

        }

        public void LoadPlaylist(int playlistId)
        {

        }

        public void Pause()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await CrossMediaManager.Current.Pause();
            });
        }

        public void PlayNext()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await CrossMediaManager.Current.PlayNext();
            });
        }

        public void PlayPrev()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await CrossMediaManager.Current.PlayPreviousOrSeekToStart();
            });
        }

        public void VolumeDown()
        {
            
            Application.Current.Dispatcher.BeginInvokeOnMainThread(() => {
                CrossMediaManager.Current.Volume.MaxVolume = 1;

                CrossMediaManager.Current.Volume.CurrentVolume = 0;
            }
            );
        }

        public void VolDown()
        {
            CrossMediaManager.Current.Volume.CurrentVolume = 0;
        }

        public void VolumeUp()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                

            });
        }

      
        public void Mute()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                
            });
        }
        public int Seek(float seekValue)
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {

            });
            return 1;
        }
        //public void Start()
        //{
        //    lock(ServerIsRunningLock)
        //    {
        //        ServerIsRunning = true;
        //        Thread player = new Thread(new ThreadStart(MediaPlayerLoop));
        //        player.Start();
        //    }

        //}

        //private void MediaPlayerLoop()
        //{
        //    bool keepGoing = true;
        //    while(keepGoing == true)
        //    {
        //        lock(ServerIsRunningLock)
        //        {
        //            if(ServerIsRunning == false)
        //            {
        //                keepGoing = false;
        //            }
        //        }
        //        Thread.Sleep(0);
        //    }
        //}
    }
}
