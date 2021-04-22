import './App.css';
import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';

import MusicPlayer from './views/musicplayer/MusicPlayer';
import PlaylistContainer from './views/playlistcontainer/PlaylistContainer';
import PlaylistManager from './views/playlistmanager/PlaylistManager';


function App() {

  return (
    <BrowserRouter>
      <h1 className="text-center m-5">Jukebox</h1>
      <Route exact path="/">

        <MusicPlayer />
        <PlaylistContainer />
        
      </Route>

      <Route path="/playlist">

        <PlaylistManager />
        <PlaylistContainer />

      </Route>
    </BrowserRouter>
  );
}

export default App;
