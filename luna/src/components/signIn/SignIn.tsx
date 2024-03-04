import React from "react";
import './SignIn.css';

interface IProps {

}

interface IState {

}

export default class SignIn extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
    }

    render() {
        return (
            <div>
                {/*header*/}
                <div className="App-Header">
                    <div className="App-Logo-Block">
                        <img className="App-Icon" src="/resources/icon.png" alt=""/>
                        <label>Luna</label>
                    </div>
                    <div className="App-Header-Language">
                        <label>Language</label>
                    </div>
                </div>
                {/*<h1>Sign In</h1>*/}
            </div>
        );
    }

}