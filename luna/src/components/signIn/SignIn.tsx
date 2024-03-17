import React from "react";
import './SignIn.css';
import Form from "react-bootstrap/Form";
import {Button} from "react-bootstrap";
import AuthProvider from "../../provider/auth/authProvider";

interface IProps {
    auth: Function;
}

interface IState {
    email: string;
    password: string;
    error: string;
}

export default class SignIn extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            email: "aboba@aboba.com",
            password: "aboba",
            error: ""
        }
    }

    signIn = async () => {

        let res = await AuthProvider.signIn(this.state.email, this.state.password)

        if(res){
            this.props.auth();
            console.log('authed')
        }else{
            this.setState({error: "Неверный логин или пароль"});
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
                            <h1>Sign In</h1>
                        </div>
                        <div className="App-Body-Content">
                            <Form>
                                <Button className="btn btn-light btn-outline-secondary Outline-Button">Google</Button>
                                <Button className="btn btn-light btn-outline-secondary Outline-Button">Not
                                    Google</Button>
                                <hr/>
                                <Form.Control type="email"
                                              placeholder="Enter email here..."
                                              value={this.state.email}
                                              onChange={(e) => {
                                                  this.setState({email: e.target.value})
                                              }}/>
                                <Form.Control type="password"
                                              placeholder="Enter password here..."
                                              value={this.state.password}
                                              onChange={(e) => {
                                                  this.setState({password: e.target.value})
                                              }}/>
                                <Button className="btn Primary-Button" onClick={async () => {
                                    await this.signIn();
                                }}>Continue</Button>
                            </Form>
                        </div>

                        <div className="App-Body-Footer">
                            <label>Forgot pass?</label>
                            <label>privacy</label>
                        </div>

                    </div>
                </div>

            </div>


        );
    }

}