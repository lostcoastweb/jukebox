
import { render } from '@testing-library/react';
import React from 'react';
import {Row, Col, Container} from 'react-bootstrap';

import '../../App.css';

const axios = require('axios').default;

const PlaylistManager = () => {

    const [songList, setSongList] = React.useState([
        {'artist': 'Dio', 'name': 'Holy Diver'},
        {'artist': 'Metallica', 'name': 'One'},
        {'artist': 'Metallica', 'name': 'Master of Puppets'},
        {'artist': 'Ne Obliviscaris', 'name': 'And Plague Flowers The Kaleidoscope'},
    ]);

    const [playlistSongs, setPlaylistSongs] = React.useState([]);

    const onMove = (item) => {
        if (songList.findIndex(j => j === item) !== -1) {
            setPlaylistSongs([...playlistSongs, item]);
            setSongList(songList.filter(j => j !== item));
        } else if (playlistSongs.findIndex(k => k === item) !== -1) {
            setSongList([...songList, item]);
            setPlaylistSongs(playlistSongs.filter(k => k !== item));
        }
    };

    return (
        <Row>
            <Col className="margin-40px">
                <Container className="centered player-border mt-5">
                    Songs
                    <hr/>
                    <SongList list={songList} onMove={onMove}  />
                </Container>
            </Col>

            <Col className="margin-40px">
                <Container className="centered player-border mt-5">
                    Playlist
                    <hr/>
                    <SongList list={playlistSongs} onMove={onMove} />
                </Container>
            </Col>
            
        </Row>
    );
};

const SongList = ({list, onMove}) => {
    return list.map(item => <SongItem item={item} onMove={onMove} />);
};

const SongItem = ({item, onMove}) => {
    const moveItem = () => {
        onMove(item);
    };

    return (
        <Container>
            <span>{item.artist} - {item.name}</span>
            <button onClick={moveItem.bind(null, item)}></button>
        </Container>
    );
};

export default PlaylistManager;