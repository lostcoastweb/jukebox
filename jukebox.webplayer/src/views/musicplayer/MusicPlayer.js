import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import config from '../../appsettings.json'

import '../../App.css';
import { useState, useEffect } from 'react';
const localIpUrl = require('local-ip-url');

const axios = require('axios').default;


export function play(event){
     event.preventDefault();
    const playBtn = document.getElementById("playBtn");
    const pauseBtn = document.getElementById("pauseBtn");
    const title = document.getElementById("titleInfo");
<<<<<<< Updated upstream
=======
    const artist = document.getElementById("artistInfo");
    const album = document.getElementById("albumInfo");
    const durationSeconds = document.getElementById("durationSecInfo");
    const durationMinutes = document.getElementById("durationMinInfo");
>>>>>>> Stashed changes
   

     playBtn.classList.add('hidden');
     playBtn.classList.remove('show');
     pauseBtn.classList.add('show');
     console.log(localIpUrl());
 
     axios.get(config.Routes.Play)
     .then((response)=>{
<<<<<<< Updated upstream
       console.log(response.data); 
       title.innerHTML = response.data;
    })
=======
       console.log(response.data.Title); 
       title.innerHTML = response.data.Title;
       artist.innerHTML = response.data.Artist;
       album.innerHTML = response.data.Album;
       if(parseInt(response.data.durationSeconds)< 10)
       {
         var addZeroNum = "0"+response.data.durationSeconds;
         durationSeconds.innerHTML = addZeroNum;
         durationMinutes.innerHTML = response.data.durationMinutes;  
       }
       else{
          durationSeconds.innerHTML = response.data.durationSeconds;
          durationMinutes.innerHTML = response.data.durationMinutes;  
       }    
      })
>>>>>>> Stashed changes
        .catch(function (){console.log("error play")});
    console.log("pressed play ");
    return 1;
    }

 export function pause(event){
     event.preventDefault();
    const playBtn = document.getElementById("playBtn");
    const pauseBtn = document.getElementById("pauseBtn");
<<<<<<< Updated upstream
    
=======
    const title = document.getElementById("titleInfo");
    const artist = document.getElementById("artistInfo");
    const album = document.getElementById("albumInfo");
    const durationSeconds = document.getElementById("durationSecInfo");
    const durationMinutes = document.getElementById("durationMinInfo");

>>>>>>> Stashed changes
     pauseBtn.classList.remove('show');
     pauseBtn.classList.add('hidden');
     playBtn.classList.add('show');

     axios.get(config.Routes.Pause)
        .then((response) => {
<<<<<<< Updated upstream
           console.log(response.data + "pause")
=======
           console.log(response.data.Title);
           title.innerHTML = response.data.Title;
       artist.innerHTML = response.data.Artist;
       album.innerHTML = response.data.Album;
       if(parseInt(response.data.durationSeconds)< 10)
       {
         var addZeroNum = "0"+response.data.durationSeconds;
         durationSeconds.innerHTML = addZeroNum;
         durationMinutes.innerHTML = response.data.durationMinutes;  
       }
       else{
          durationSeconds.innerHTML = response.data.durationSeconds;
          durationMinutes.innerHTML = response.data.durationMinutes;  
       }
>>>>>>> Stashed changes
         }
      ).catch(function (error){console.log("error pause")});
    console.log("pressed pause");
}
 export function playNext(event){
<<<<<<< Updated upstream
=======
   const title = document.getElementById("titleInfo");
    const artist = document.getElementById("artistInfo");
    const album = document.getElementById("albumInfo");
    const durationSeconds = document.getElementById("durationSecInfo");
    const durationMinutes = document.getElementById("durationMinInfo");
>>>>>>> Stashed changes
     event.preventDefault();
   
     axios.get(config.Routes.Next)
        .then((response) => {
<<<<<<< Updated upstream
           console.log(response.data + "next")
         }
=======
           console.log(response.data + "next");
           title.innerHTML = response.data.Title;
       artist.innerHTML = response.data.Artist;
       album.innerHTML = response.data.Album;
       if(parseInt(response.data.durationSeconds)< 10)
       {
         var addZeroNum = "0"+response.data.durationSeconds;
         durationSeconds.innerHTML = addZeroNum;
         durationMinutes.innerHTML = response.data.durationMinutes;  
       }
       else{
          durationSeconds.innerHTML = response.data.durationSeconds;
          durationMinutes.innerHTML = response.data.durationMinutes;  
       }      
       }
>>>>>>> Stashed changes
      ).catch(function (error){console.log("error next")});
    console.log("pressed next");

}
 export function playPrev(event){
<<<<<<< Updated upstream
=======
   const title = document.getElementById("titleInfo");
    const artist = document.getElementById("artistInfo");
    const album = document.getElementById("albumInfo");
    const durationSeconds = document.getElementById("durationSecInfo");
    const durationMinutes = document.getElementById("durationMinInfo");

>>>>>>> Stashed changes
     event.preventDefault();

     axios.get(config.Routes.Prev)
        .then((response) => {
           console.log(response.data + "prev")
<<<<<<< Updated upstream
         }
      ).catch(function (error){console.log("error prev")});
=======
           title.innerHTML = response.data.Title;
       artist.innerHTML = response.data.Artist;
       album.innerHTML = response.data.Album;
       if(parseInt(response.data.durationSeconds)< 10)
       {
         var addZeroNum = "0"+response.data.durationSeconds;
         durationSeconds.innerHTML = addZeroNum;
         durationMinutes.innerHTML = response.data.durationMinutes;  
       }
       else{
          durationSeconds.innerHTML = response.data.durationSeconds;
          durationMinutes.innerHTML = response.data.durationMinutes;  
       }
             
       }

      ).catch(function (error){console.log("error")});
>>>>>>> Stashed changes
    console.log("pressed prev");

}

function MusicPlayer() {
  const [volume, setVolume] = useState(50);

  function volDown(event){
     event.preventDefault();
    setVolume(parseInt(volume) - 10);
     axios.get(config.Routes.volDown)
        .then((response) => {
           console.log(response.data + "volDown")
         }
      ).catch(function (error){console.log("error volDown")});
    console.log("pressed volDown");
    console.log(volume);

  }
  function volUp(event){
     event.preventDefault();
     setVolume(parseInt(volume)  + 10);

     axios.get(config.Routes.volUp)
        .then((response) => {
           console.log(response.data + "volUp")
         }
      ).catch(function (error){console.log("error volUp")});
    console.log("pressed volUp");
    console.log(volume);

  }
  function mute(event){
     event.preventDefault();
     setVolume(0); 

     axios.get(config.Routes.mute)
        .then((response) => {
           console.log(response.data + "mute")
         }
      ).catch(function (error){console.log("error mute")});
    console.log("pressed mute");

  }

   useEffect(() => {   
    const volumeSlider = document.getElementById("volume-meter");
    volumeSlider.value=volume;
  },[volume]);

  function onChangeVolume(){
    var volumeInput = document.getElementById('volume-meter');
    setVolume(volumeInput.value); 
    console.log(volumeInput.value);
    console.log(volume);
  };
  function onSeek(){
    var seekInput = document.getElementById('seekbar');
    var durationSec = parseInt(document.getElementById('durationSecInfo').innerHTML);
    var durationMin = parseInt(document.getElementById('durationMinInfo').innerHTML);
    var totalSeconds = (durationMin*60 )+ durationSec;
    var newSeekTime = (seekInput.value/100)*totalSeconds;

    console.log("SeekTime:" + newSeekTime);
    axios.get(config.Routes.seek+"?seekValue="+newSeekTime)
        .then((response) => {
           console.log(response.data + "seek")
         }
      ).catch(function (error){console.log("error seek")});
    //console.log("seek" + seekInput.value);
    //var newSongTime = duration/seekInput;
    //console.log(newSongTime);
   
  };
  
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

              
                {/* SeekBar */}
<<<<<<< Updated upstream
                <Row className="justify-content-center m-5">
                  <input className="seekbar" id="seekbar"  type="range" min="1" max="100" onChange={onSeek}/>                  
                </Row>
=======
                 <div>
                   
                  <Row className="justify-content-center m-5">
                      <input className="seekbar" id="seekbar"  type="range" min="1" max="100" onChange={onSeek}/>   
                      
                      <div className="pb-5" id="durationMinInfo"> duration </div>               
                      <br></br>
                      <div>:</div>  
                      <div className="pb-5" id="durationSecInfo"> duration </div>    
                           
                  </Row>
>>>>>>> Stashed changes

                {/* Track Control Buttons */}
                <Row className="justify-content-center my-5 mx-1">
                      <Button className="m-3 prevBtn playerBtn"  onClick={e=>playPrev(e)} id="prevBtn" ></Button>                
                      <Button className="m-3 pauseBtn hidden playerBtn" alt="pause" onClick={e=>pause(e)} id="pauseBtn"></Button>              
                      <Button className="m-3 playBtn playerBtn" alt="play" id="playBtn" onClick={e=>play(e)}></Button>          
                      <Button className="m-3 nextBtn playerBtn"  onClick={e=>playNext(e)} id="nextBtn"></Button>
                </Row>

                  {/* Volume Buttons */}
                 <Row className="justify-content-center ">
                      <Button className="m-3 volDownBtn playerBtn"  onClick={e=>volDown(e)} id="volDown"></Button>                     
                      <input className="volume-meter" id="volume-meter"  type="range" min="1" max="100" onChange={onChangeVolume}/>                  
                      <Button className="m-3 volUpBtn playerBtn"  onClick={e=>volUp(e)} id="volUp"></Button>
                      <Button className="m-3 muteBtn playerBtn"  onClick={e=>mute(e)} id="mute"></Button>                 
                </Row>
           

        </Container>

        </>

       
    );
    

}
    
    

export default MusicPlayer;
