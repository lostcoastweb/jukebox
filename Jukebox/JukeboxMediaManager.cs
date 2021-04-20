using Jukebox.Models;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;


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
