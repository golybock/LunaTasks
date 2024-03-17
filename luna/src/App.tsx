import React from 'react';
import {Routes, Route, BrowserRouter} from "react-router-dom"
import {Navbar} from "./components/navbar/Navbar";
import SignIn from "./components/signIn/SignIn";
import Home from "./components/home/Home";
import Account from "./components/account/Account";
import About from "./components/about/About";
import './App.css';
import UserProvider from "./provider/user/userProvider";

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

    async componentDidMount() {
        let user = await UserProvider.getMe();

        if(user != null){
            this.auth()
            return;
        }

        if(window.location.pathname != "/signIn"){
            window.location.replace("/signIn")
        }
    }

    auth() {
        this.setState({isAuthed: true})

        if(window.location.pathname != "/"){
            window.location.replace("/")
        }
    }

    signOut() {
        localStorage.clear()
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
                        <Route path="/signIn" element={<SignIn auth={() => this.auth()}/>}/>
                        <Route path="*" element={<p>There's nothing here: 404!</p>}/>
                    </Routes>
                )}
            </BrowserRouter>
        );
    }

}

