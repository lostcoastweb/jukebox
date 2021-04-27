
import { render } from '@testing-library/react';
import React from 'react';
import { Row, Col, Container } from 'react-bootstrap';

import '../../App.css';
import { play } from '../musicplayer/MusicPlayer';

const axios = require('axios').default;

const SearchSongs = (searchTerm, setList) => {
    axios.get(`http://localhost:8080/api/media/search/${searchTerm}`).then((response) => {
        setList(JSON.parse(response.data.slice(1, response.data.length-1)));
    }).catch((reason) => {
        console.error("Error encountered: ", reason);
    });
};

const GetSongs = (setList) => {
    axios.get('http://localhost:8080/api/music/files').then((response) => {
        setList(JSON.parse(response.data.slice(1, response.data.length-1)));
    }).catch((reason) => {
        console.error("Error encountered: ", reason);
    });
};

const PlaylistManager = () => {

    const [songList, setSongList] = React.useState([]);

    const [playlistSongs, setPlaylistSongs] = React.useState([]);

    const [searchTerm, setSearchTerm] = React.useState('');

    // todo: implement sort
    /*const alphaNumericSort = (a, b) => {
        
    };*/

    const onMove = (item) => {
        if (/*songList !== undefined && songList !== null &&*/ songList.findIndex(j => j === item) !== -1) {
            console.log("1");
            setPlaylistSongs([...playlistSongs, item]);
            setSongList(songList.filter(j => j !== item));
        } else if (/*playlistSongs !== undefined && playlistSongs !== null &&*/ playlistSongs.findIndex(k => k === item) !== -1) {
            console.log("2S");
            setSongList([...songList, item]);
            setPlaylistSongs(playlistSongs.filter(k => k !== item));
        }
    };

    const onSearch = (event) => {
        event.preventDefault();
    };

    const searchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    React.useEffect(() => {
        if (searchTerm !== '' && searchTerm !== undefined && searchTerm !== null) {
            setSongList(SearchSongs(searchTerm, setSongList));
        } else {
            GetSongs(setSongList);
        }
    }, [searchTerm]);

    return (
        <Row>
            <Col className="margin-40px">
                <Container className="centered player-border mt-5">
                    Songs
                    <hr />
                    <form onSubmit={onSearch}>Search: <input type="search" value={searchTerm} style={{ margin: '10px' }} onChange={searchChange}/></form>
                    {songList === undefined ? (<>True</>) : (<SongList list={songList} onMove={onMove} />)}
                    {/**/}
                </Container>
            </Col>

            <Col className="margin-40px">
                <Container className="centered player-border mt-5">
                    Playlist
                    <hr />
                    <SongList list={playlistSongs} onMove={onMove} />
                </Container>
            </Col>

        </Row>
    );
};

const SongList = ({ list, onMove }) => {
    return list.map(item => <SongItem item={item} onMove={onMove} />);
};

const SongItem = ({ item, onMove }) => {
    const moveItem = () => {
        onMove(item);
    };

    return (
        <Container>
            <span>{item["Artist"]} - {item["Title"]}</span>
            <button onClick={moveItem.bind(null, item)}></button>
        </Container>
    );
};

export default PlaylistManager;