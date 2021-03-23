import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import '../../App.css';
const axios = require('axios').default;



function MusicPlayer() {
    axios.post('/pause')
        .then((response) => {
           console.log(response.data)
         }
      );
    axios.post('/play')
        .then((response) => {
           console.log(response.data)
         }
      );


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
                   
                        <Button className="m-3 pauseBtn playerBtn"></Button>
                      
                        <Button className="m-3 playBtn playerBtn"></Button>
                    
                        <Button className="m-3 nextBtn playerBtn"></Button>
                
                </Row>
           

        </Container>

        </>

       
    );



}



export default MusicPlayer;
