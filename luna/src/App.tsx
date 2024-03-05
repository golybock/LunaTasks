import React from 'react';
import {Routes, Route, BrowserRouter} from "react-router-dom"
import {Navbar} from "./components/navbar/Navbar";
import SignIn from "./components/signIn/SignIn";
import Home from "./components/home/Home";
import Account from "./components/account/Account";
import About from "./components/about/About";
import './App.css';


interface IProps {

}

interface IState {
    isAuthed: boolean;
}

export default class Auth extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            isAuthed: false
        }
    }

    auth() {
        this.setState({isAuthed: true})
    }

    signOut() {
        this.setState({isAuthed: false})
    }

    render() {
        return (
            <BrowserRouter>
                {this.state.isAuthed && (
                    <Routes>
                        <Route element={<Navbar signOut={() => this.signOut()}/>}>
                            <Route path="/" element={<Home/>}/>
                            <Route path="account" element={<Account/>}/>
                            <Route path="about" element={<About/>}/>
                        </Route>
                        <Route path="*" element={<p>There's nothing here: 404!</p>}/>
                    </Routes>
                )}
                {!this.state.isAuthed && (
                    <Routes>
                        <Route path="*" element={<SignIn auth={() => this.auth()}/>}/>
                    </Routes>
                )}
            </BrowserRouter>
        );
    }

}

