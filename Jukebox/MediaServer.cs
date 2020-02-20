using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Jukebox.Library
{
    public class MediaServer
    {
        private bool ServerIsRunning = false;
        private readonly object ServerIsRunningLock = new object();

        private bool IsAudioPlaying = false;
        private readonly object IsAudioPlayingLock = new object();

        public MediaServer()
        {

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
