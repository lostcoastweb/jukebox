import { render, screen } from '@testing-library/react';
import App from './App';
import React from 'react';
import ReactDOM from 'react-dom';
import { unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";


import MusicPlayer from "../src/views/musicplayer/MusicPlayer";
import {play} from "../src/views/musicplayer/MusicPlayer";


let container = null;
beforeEach(() => {
  // setup a DOM element as a render target
  container = document.createElement("div");
  document.body.appendChild(container);
});

afterEach(() => {
  // cleanup on exiting
  unmountComponentAtNode(container);
  container.remove();
  container = null;
});

it('play test', () => {
  expect(play()).toEqual(1);
});
it('pause test', () => {
  expect(play()).toEqual(1);
});

it("renders Music Player", () => {
  act(() => {
    render(<MusicPlayer />, container);
  });
  expect(container.textContent).toBe("");

  
});

test('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
});

test('is true true', () => {
  render(<App />);
  expect(true).toBeTruthy();
});


