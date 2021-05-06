
import React from 'react';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';

import {Link, BrowserRouter, Route} from 'react-router-dom';

import '../../App.css';
const axios = require('axios').default;

// Using this format for function, is this incorrect?
const nextPlaylist = (setPlaylist) => {
    axios.get('http://localhost:8080/api/playlist/next')
        .then((response) => {
            console.log("Playlist " + response.data + " started.")
            setPlaylist(response.data);
        }).catch((err) => { console.log("Failed to start next playlist: ", err) });
};

const previousPlaylist = (setPlaylist) => {
    axios.get('http://localhost:8080/api/playlist/previous')
        .then((response) => {
            console.log("Playlist " + response.data + " started.")
            setPlaylist(response.data);
        }).catch((err) => { console.log("Failed to start previous playlist: ", err) });
};

const inputName = () => {
    const input = document.getElementById("playlist_name");
    if (input.classList.contains("hidden")) {
        input.classList.remove("hidden");
        input.classList.add("show");
    } else {
        input.classList.remove("show");
        input.classList.add("hidden");
    }
};

const newPlaylist = (name, playlists, setCurrPlaylist, setPlaylists, setNewName) => {
    console.log("New playlist created.");
    var new_playlists = playlists;
    setCurrPlaylist(playlists.length);
    new_playlists.push({'name': name});
    setPlaylists(new_playlists);
    setNewName("");

    axios.post('http://localhost:8080/api/playlist/new')
        .then((response) => {
            
        }).catch((err) => { console.log("Failed to create new playlist: ", err)})
};

const PlaylistContainer = () => {

    const [playlists, setPlaylists] = React.useState([
        {'name': "My playlist 1", tracks: [{"title": "Holy Diver", 'artist': 'Dio'}]},
        {'name': "Metal playlist"},
    ]
    );

    const [currPlaylist, setCurrPlaylist] = React.useState(0);

    const [newName, setNewName] = React.useState("");

    const newChange = (event) => {
        setNewName(event.target.value);
    }

    const handleNew = (event) => {
        event.preventDefault();
        newPlaylist(newName, playlists, setCurrPlaylist, setPlaylists, setNewName);
        inputName();
    };

    const handleNext = () => {
        nextPlaylist(setPlaylists);
    };

    const handlePrev = () => {
        previousPlaylist(setPlaylists);
    };

    return (
        <Container className="centered player-border mt-5">

            <Row className="justify-content-center m-5">
                { playlists[currPlaylist] === {} ? (<div>Playlist - None</div>) : (<div>Playlist - {playlists[currPlaylist].name}</div>) }
            </Row>

            <Row id="playlist_name" className="justify-content-center m-5 hidden">

                <form onSubmit={handleNew}>
                        <input value={newName} onChange={newChange}></input>
                        <button type="submit">Save</button>
                </form>

            </Row>

            <Row className="justify-content-center m-5">

                <Button className="m-3 plusBtn playerBtn" onClick={inputName}></Button>
                <Button className="m-3 prevBtn playerBtn" onClick={handlePrev}></Button>
                <Button className="m-3 nextBtn playerBtn" onClick={handleNext}></Button>
                <Link className="m-4" to="/playlist">Edit</Link>

            </Row>

            <Row className="justify-content-center m-5">
                <List list={playlists}></List>
            </Row>
        </Container>
    );

};

const List = ({list}) => (
    list.map(item => <PlaylistItem item={item} />)
);

const PlaylistItem = ({item}) => {
    return (
        <Container>
            <span>{item.name}</span>
        </Container>
    );
};

export default PlaylistContainer;