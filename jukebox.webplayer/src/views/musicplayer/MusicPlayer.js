import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import React from 'react';

import '../../App.css';
import PlaylistContainer from '../playlistcontainer/PlaylistContainer';
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

function MusicPlayer() {

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

            <PlaylistContainer></PlaylistContainer>

        </>

       
    );



}

export default MusicPlayer;
