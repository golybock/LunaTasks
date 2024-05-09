import React from 'react';
import {Routes, Route} from "react-router-dom"
import {LeftNavbar} from "./components/navbar/LeftNavbar";
import SignIn from "./components/auth/SignIn";
import Home from "./components/home/Home";
import Account from "./components/account/Account";
import Settings from "./components/settings/Settings";
import Page from "./components/page/Page";
import {NotAuthedRoute, ProtectedRoute} from "./auth/AuthWrapper";
import InviteWorkspacePage from "./components/settings/InviteWorkspacePage";
import SignUp from "./components/auth/SignUp";
import './App.css';
import './dark.css';
import ChartsPage from "./components/charts/ChartsPage";
import EditAccount from "./components/account/EditAccount";

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

        // installer
        window.addEventListener("beforeinstallprompt", (e : any) => {
            console.log(e.platforms); // e.g., ["web", "android", "windows"]
            e.userChoice.then((choiceResult: any) => {
                console.log(choiceResult.outcome); // either "accepted" or "dismissed"
            });
        });
    }

    render() {
        return (
                <Routes>
                    <Route element={<ProtectedRoute outlet={<LeftNavbar/>}/>}>
                        <Route index path="/" element={<Home/>}/>
                        <Route path="page/:pageId" element={<Page/>}/>
                        <Route path="account" element={<Account/>}/>
                        <Route path="editAccount" element={<EditAccount/>}/>
                        <Route path="statistic" element={<ChartsPage/>}/>
                        <Route path="settings" element={<Settings/>}/>
                    </Route>
                    <Route path="inviteWorkspace/:workspaceId" element={<ProtectedRoute outlet={<InviteWorkspacePage/>}/>}/>
                    <Route path="*" element={<p>There's nothing here: 404!</p>}/>
                    <Route path="/signIn" element={<NotAuthedRoute outlet={<SignIn/>}/>}/>
                    <Route path="/signUp" element={<NotAuthedRoute outlet={<SignUp/>}/>}/>

                </Routes>
        );
    }
}

