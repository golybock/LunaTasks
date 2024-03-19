import React from 'react';
import {Routes, Route} from "react-router-dom"
import {Navbar} from "./components/navbar/Navbar";
import SignIn from "./components/signIn/SignIn";
import Home from "./components/home/Home";
import Account from "./components/account/Account";
import About from "./components/about/About";
import './App.css';
import Page from "./components/page/Page";
import Loading from "./components/notifications/Loading";
import {NotAuthedRoute, ProtectedRoute} from "./auth/AuthWrapper";

interface IProps {

}

interface IState {
    isLoading: boolean;
}


export default class Auth extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            isLoading: true
        }
    }


    componentDidMount() {
        this.setState({isLoading: false})
    }

    render() {
        return (
            <Routes>
                <Route element={<Navbar/>}>
                    <Route index path="/" element={<ProtectedRoute outlet={<Home/>}/>}/>
                    <Route path="page" element={<ProtectedRoute outlet={<Page/>}/>}/>
                    <Route path="account" element={<ProtectedRoute outlet={<Account/>}/> }/>
                    <Route path="about" element={<ProtectedRoute outlet={<About/>}/>} />
                </Route>
                <Route path="*" element={<p>There's nothing here: 404!</p>}/>
                <Route path="/signIn" element={<NotAuthedRoute outlet={<SignIn/>}/>}/>

            </Routes>
        );
    }

}

