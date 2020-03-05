import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import './App.css';

function App() {
  return (
    <div className="App">
      <Navbar bg="dark" variant="dark" fixed="top">
        <Navbar.Brand href="#home">Jukebox</Navbar.Brand>
        <Nav className="mr-auto">
          <Nav.Link href="#home">Playlists</Nav.Link>
          <Nav.Link href="#home">About</Nav.Link>
        </Nav>
      </Navbar>
    </div>
  );
}

export default App;
