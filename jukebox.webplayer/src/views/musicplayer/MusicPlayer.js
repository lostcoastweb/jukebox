import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import config from '../../appsettings.json'

import '../../App.css';
import { useState } from 'react';
const localIpUrl = require('local-ip-url');

const axios = require('axios').default;



 export function play(event){
     event.preventDefault();
    const playBtn = document.getElementById("playBtn");
    const pauseBtn = document.getElementById("pauseBtn");
   

     playBtn.classList.add('hidden');
     playBtn.classList.remove('show');
     pauseBtn.classList.add('show');
     console.log(localIpUrl());
 
     axios.get(config.Routes.Play)
     .then((response)=>{console.log(response.data)})
        .catch(function (){console.log("error play")});
    console.log("pressed play ");
    return 1;
    }

 export function pause(event){
     event.preventDefault();
    const playBtn = document.getElementById("playBtn");
    const pauseBtn = document.getElementById("pauseBtn");
    
     pauseBtn.classList.remove('show');
     pauseBtn.classList.add('hidden');
     playBtn.classList.add('show');

     axios.get(config.Routes.Pause)
        .then((response) => {
           console.log(response.data + "pause")
         }
      ).catch(function (error){console.log("error pause")});
    console.log("pressed pause");
}
 export function playNext(event){
     event.preventDefault();
   
     axios.get(config.Routes.Next)
        .then((response) => {
           console.log(response.data + "next")
         }
      ).catch(function (error){console.log("error next")});
    console.log("pressed next");

}
 export function playPrev(event){
     event.preventDefault();

     axios.get(config.Routes.Prev)
        .then((response) => {
           console.log(response.data + "prev")
         }
      ).catch(function (error){console.log("error prev")});
    console.log("pressed prev");

}
 export function volDown(event){
     event.preventDefault();

     axios.get(config.Routes.volDown)
        .then((response) => {
           console.log(response.data + "volDown")
         }
      ).catch(function (error){console.log("error volDown")});
    console.log("pressed volDown");

}
 export function volUp(event){
     event.preventDefault();

     axios.get(config.Routes.volUp)
        .then((response) => {
           console.log(response.data + "volUp")
         }
      ).catch(function (error){console.log("error volUp")});
    console.log("pressed volUp");

}
 export function mute(event){
     event.preventDefault();

     axios.get(config.Routes.mute)
        .then((response) => {
           console.log(response.data + "mute")
         }
      ).catch(function (error){console.log("error mute")});
    console.log("pressed mute");

}

function MusicPlayer() {

    return(
        <>
        <Container className="centered player-border mt-5">
                   
                {/* Song Information */}
                <Row className="justify-content-center m-5">
                    <div className="songTitle">
                        <div id="titleInfo">Track - song 1</div>
                        <br></br>
                        <div id="artistInfo">Artist - someones</div>
                    </div>   
                </Row>

                {/* Music Player Buttons */}
                <Row className="justify-content-center m-5">
                      <Button className="m-3 prevBtn playerBtn"  onClick={e=>playPrev(e)} id="prevBtn" ></Button>
                   
                        <Button className="m-3 pauseBtn hidden playerBtn" alt="pause" onClick={e=>pause(e)} id="pauseBtn"></Button>
                      
                        <Button className="m-3 playBtn playerBtn" alt="play" id="playBtn" onClick={e=>play(e)}></Button>
                    
                        <Button className="m-3 nextBtn playerBtn"  onClick={e=>playNext(e)} id="nextBtn"></Button>
                      

                      
                        <Button className="m-3 volDownBtn playerBtn"  onClick={e=>volDown(e)} id="volDown"></Button>
                        <Button className="m-3 volUpBtn playerBtn"  onClick={e=>volUp(e)} id="volUp"></Button>
                        <Button className="m-3 muteBtn playerBtn"  onClick={e=>mute(e)} id="mute"></Button>  
                        
                
                </Row>
           

        </Container>

        </>

       
    );



}



export default MusicPlayer;
