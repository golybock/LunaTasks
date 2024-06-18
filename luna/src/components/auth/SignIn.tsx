import React from "react";
import Form from "react-bootstrap/Form";
import {Button} from "react-bootstrap";
import AuthProvider from "../../provider/auth/authProvider";
import {NavLink} from "react-router-dom";
import NotificationManager from "../../tools/NotificationManager";
import './SignIn.css';
import ImageManager from "../../tools/ImageManager";

interface IProps {
}

interface IState {
    email: string;
    password: string;
}

export default class SignIn extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            email: "admin@admin.com",
            password: "admin",
        }
    }

    signIn = async () => {

        let res = await AuthProvider.signIn(this.state.email, this.state.password)

        if (res) {
            console.log('authed')
        } else {
            NotificationManager.makeError("Ошибка авторизации")
        }
    }

    render() {
        return (
            <div className="App">

                <div className="Image-Block">
                    <div className="App-header">
                        <img src={ImageManager.getImageSrc("background-4.jpg")} className="App-logo" alt="logo"/>
                    </div>
                </div>

                <div className="SignIn-Block">

                    {/*header*/}
                    <div className="App-Header">
                        <div className="App-Logo-Block">
                            <img className="App-Icon" src="/resources/icon.png" alt=""/>
                            <label>Luna</label>
                        </div>
                        <div className="App-Header-Language">
                            <label>English</label>
                        </div>
                    </div>

                    <div className="App-Body">

                        <div className="App-Body-Header">
                            <h1>Sign In</h1>
                        </div>

                        <div className="App-Body-Content">
                            <Form>

                                {/*<Button className="btn btn-light btn-outline-secondary Outline-Button">Google</Button>*/}
                                {/*<Button className="btn btn-light btn-outline-secondary Outline-Button">Not Google</Button>*/}

                                {/*<hr/>*/}

                                <Form.Control type="email"
                                              className="Form-Control"
                                              placeholder="Enter email here..."
                                              value={this.state.email}
                                              onChange={(e) => {
                                                  this.setState({email: e.target.value})
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
                                            await this.signIn();
                                        }}>
                                    Continue
                                </Button>

                            </Form>
                        </div>

                        <div className="App-Body-Footer">
                            <NavLink to="/signUp">
                                <label>No account?</label>
                            </NavLink>
                            {/*<label>Forgot pass?</label>*/}
                            {/*<label>privacy</label>*/}
                        </div>

                    </div>
                </div>

            </div>


        );
    }

}