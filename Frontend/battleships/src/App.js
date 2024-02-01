// import logo from './logo.svg';
import './App.css';
import { router } from "./router";
import { RouterProvider } from "react-router-dom";
import React from 'react';

function App() {
  return (
    <div className="App">
      <RouterProvider router={router} />
    </div>
  );
}

export default App;
