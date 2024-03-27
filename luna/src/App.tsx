import React from 'react';
import {Routes, Route} from "react-router-dom"
import {LeftNavbar} from "./components/navbar/LeftNavbar";
import SignIn from "./components/signIn/SignIn";
import Home from "./components/home/Home";
import Account from "./components/account/Account";
import About from "./components/about/About";
import Page from "./components/page/Page";
import {NotAuthedRoute, ProtectedRoute} from "./auth/AuthWrapper";
import './App.css';

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
                <Route element={ <ProtectedRoute outlet={<LeftNavbar/>}/>}>
                    <Route index path="/" element={<Home/>}/>
                    <Route path="page" element={<Page/>}/>
                    <Route path="account" element={<Account/>}/>
                    <Route path="about" element={<About/>} />
                </Route>
                <Route path="*" element={<p>There's nothing here: 404!</p>}/>
                <Route path="/signIn" element={<NotAuthedRoute outlet={<SignIn/>}/>}/>

            </Routes>
        );
    }

}

