import React from 'react';
import './App.css';
import SignIn from "./components/signIn/SignIn";

function App() {
    return (
        <div className="container">
            <div className="App">
                <header className="App-header">
                    <img src="/resources/background-transparent.png" className="App-logo" alt="logo"/>
                </header>
            </div>

            <div className="SignIn">
                <SignIn/>
            </div>

        </div>
    );
}

export default App;
