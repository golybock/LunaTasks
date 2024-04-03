import React from "react";
import Form from "react-bootstrap/Form";
import {Button} from "react-bootstrap";
import AuthProvider from "../../provider/auth/authProvider";
import './SignUp.css';
import {NavLink} from "react-router-dom";

interface IProps {
    // auth: Function;
}

interface IState {
    email: string;
    password: string;
    username: string;
    error: string;
}

export default class SignUp extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            email: "aboba@aboba.com",
            password: "aboba",
            error: "",
            username: ""
        }
    }

    getBlank(){
        return {email: this.state.email, password: this.state.password, username: this.state.username};
    }

    signUp = async () => {

        let res = await AuthProvider.signUp(this.getBlank())

        if (res) {
            console.log('authed')
        } else {
            this.setState({error: "Не удалось зарегистрировать пользователя"});
        }
    }

    render() {
        return (
            <div className="App">

                <div className="Image-Block">
                    <header className="App-header">
                        <img src="/resources/background-transparent.png" className="App-logo" alt="logo"/>
                    </header>
                </div>

                <div className="SignIn-Block">
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
                    <div className="App-Body">
                        <div className="App-Body-Header">
                            <h1>Sign Up</h1>
                        </div>
                        <div className="App-Body-Content">
                            <Form>

                                <Button className="btn btn-light btn-outline-secondary Outline-Button">Google</Button>
                                <Button className="btn btn-light btn-outline-secondary Outline-Button">Not Google</Button>

                                <hr/>

                                <Form.Control type="email"
                                              className="Form-Control"
                                              placeholder="Enter email here..."
                                              value={this.state.email}
                                              onChange={(e) => {
                                                  this.setState({email: e.target.value})
                                              }}/>

                                <Form.Control type="text"
                                              className="Form-Control"
                                              placeholder="Enter username here..."
                                              value={this.state.username}
                                              onChange={(e) => {
                                                  this.setState({username: e.target.value})
                                              }}/>

                                <Form.Control type="password"
                                              className="Form-Control"
                                              placeholder="Enter password here..."
                                              value={this.state.password}
                                              onChange={(e) => {
                                                  this.setState({password: e.target.value})
                                              }}/>

                                <Button className="btn Primary-Button"
                                        onClick={async () => {
                                            await this.signUp();
                                        }}>
                                    Continue
                                </Button>

                                <>
                                    {this.state.error && (
                                        <div className="Error">
                                            <label>{this.state.error}</label>
                                        </div>
                                    )
                                    }
                                </>

                            </Form>
                        </div>

                        <div className="App-Body-Footer">
                            <NavLink to="/signIn">
                                <label>I already have account</label>
                            </NavLink>
                            <label>Forgot pass?</label>
                            <label>privacy</label>
                        </div>

                    </div>
                </div>

            </div>


        );
    }

}