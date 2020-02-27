using Jukebox.Models;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jukebox.Library
{
    public class MediaServer
    {
        private bool ServerIsRunning = false;
        private readonly object ServerIsRunningLock = new object();

        private bool IsAudioPlaying = false;
        private readonly object IsAudioPlayingLock = new object();

        private Playlist _currentPlaylist = new Playlist();

        private static MediaServer _instance;

        private MediaServer()
        {
            
        }

        private async Task Init()
        {
            await CrossMediaManager.Current.Play("http://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            await CrossMediaManager.Current.Pause();
            CrossMediaManager.Current.MediaItemFinished += SongDonePlaying;
        }

        private void SongDonePlaying(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            
        }

        public static async Task<MediaServer> GetInstance()
        {
            if(_instance == null)
            {
                _instance = new MediaServer();
                await _instance.Init();
            }
            return _instance;
        }

        public async Task Play()
        {
            await CrossMediaManager.Current.Play();
        }

        public async Task Pause()
        {
            await CrossMediaManager.Current.Pause();
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
