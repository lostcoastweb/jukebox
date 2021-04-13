import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import React from 'react';

import '../../App.css';
const axios = require('axios').default;

 function play(){
     axios.get('http://localhost:8080/api/music/play')
        .catch(function (){console.log("error play")});
    console.log("pressed play");

}

 function pause(){
     axios.get('http://localhost:8080/api/music/pause')
        .then((response) => {
           console.log(response.data + "pause")
         }
      ).catch(function (error){console.log("error pause")});
    console.log("pressed pause");

}

// Using this format for function, is this incorrect?
const nextPlaylist = (setPlaylist) => {
    axios.get('http://localhost:8080/api/media/playlist/next')
        .then((response) => {
            console.log("Playlist " + response.data + " started.")
            setPlaylist(response.data);
        }).catch((error) => { console.log("Failed to start next playlist.") });
};

const previousPlaylist = (setPlaylist) => {
    axios.get('http://localhost:8080/api/media/playlist/previous')
        .then((response) => {
            console.log("Playlist " + response.data + " started.")
            setPlaylist(response.data);
        }).catch((error) => { console.log("Failed to start previous playlist.") });
};

function MusicPlayer() {

    const [playlist, setPlaylist] = React.useState({})

    const handleNext = () => {
        nextPlaylist(setPlaylist);
    };

    const handlePrev = () => {
        previousPlaylist(setPlaylist);
    };

    return(
        <>
        <Container className="centered player-border mt-5">
                   
                {/* Song Information */}
                <Row className="justify-content-center m-5">
                    <div className="songTitle">
                        Title - song 1
                        <br></br>
                        Artist - someone
                    </div>   
                </Row>

                {/* Music Player Buttons */}
                <Row className="justify-content-center m-5">

                        <Button className="m-3 prevBtn playerBtn"></Button>
                   
                        <Button className="m-3 pauseBtn playerBtn" onClick={pause}></Button>
                      
                        <Button className="m-3 playBtn playerBtn" onClick={play}></Button>
                    
                        <Button className="m-3 nextBtn playerBtn"></Button>
                
                </Row>

                

        </Container>

        {/* Playlist section */}
        <Container className="centered player-border mt-5">
            <Row className="justify-content-center m-5">
                { playlist !== {} ? (<div>Playlist - None</div>) : (<div>Playlist - {playlist.title}</div>) }
            </Row>

            <Row className="justify-content-center m-5">

                <Button className="m-3 prevBtn playerBtn" onClick={handlePrev}></Button>
                <Button className="m-3 nextBtn playerBtn" onClick={handleNext}></Button>

            </Row>
        </Container>

        </>

       
    );



}



export default MusicPlayer;
