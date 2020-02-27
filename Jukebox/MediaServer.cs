using Jukebox.Models;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jukebox.Library
{
    class MediaServer
    {
        private bool ServerIsRunning = false;
        private readonly object ServerIsRunningLock = new object();

        private bool IsAudioPlaying = false;
        private readonly object IsAudioPlayingLock = new object();

        private Playlist _currentPlaylist = new Playlist();
        private static IMediaManager _mediaManager = null;
        private static MediaServer _instance;

        private MediaServer()
        {
            
        }

        private async Task Init(IMediaManager musicContext = null)
        {
            if(musicContext != null)
            {
                _mediaManager = musicContext;
            }
            else if(_mediaManager == null)
            {
                _mediaManager = CrossMediaManager.Current;
            }
            //await _mediaManager.Play("http://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            //await _mediaManager.Pause();
            _mediaManager.MediaItemFinished += SongDonePlaying;
        }

        private void SongDonePlaying(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            
        }

        public static async Task<MediaServer> GetInstance(IMediaManager musicContext = null)
        {
            if(_instance == null)
            {
                _instance = new MediaServer();
                await _instance.Init(musicContext);
            }
            return _instance;
        }

        public async Task Play()
        {
            await _mediaManager.Play();
        }

        public async Task Pause()
        {
            await _mediaManager.Pause();
        }

        public async void PlayFile()
        {
            //await CrossMediaManager.Current.Play("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
        }

        public void Start()
        {
            lock(ServerIsRunningLock)
            {
                ServerIsRunning = true;
                Thread player = new Thread(new ThreadStart(MediaPlayerLoop));
                player.Start();
            }
            
        }

        private void MediaPlayerLoop()
        {
            bool keepGoing = true;
            while(keepGoing == true)
            {
                lock(ServerIsRunningLock)
                {
                    if(ServerIsRunning == false)
                    {
                        keepGoing = false;
                    }
                }
                Thread.Sleep(0);
            }
        }
    }
}
