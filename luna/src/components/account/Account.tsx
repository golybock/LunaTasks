import React from "react";
import IUserView from "../../models/user/userView";
import UserProvider from "../../provider/user/userProvider";
import "./Account.css"
import {Button} from "react-bootstrap";

interface IProps {

}

interface IState {
    user: IUserView | null
}

export default class Account extends React.Component<IProps, IState> {

    headerUrl = "http://localhost:7005/woodcuts_14.jpg";

    constructor(props: IProps) {
        super(props);

        this.state = {
            user: null
        }
    }

    async componentDidMount() {
        const user = await UserProvider.getMe();

        this.setState({user: user})
    }

    render() {
        return (
            <div>
                <div className="Header-Image-Container">
                    <img src={this.headerUrl} alt=""/>
                </div>
                <div className="Content">
                    <div>
                        <div className="User-Info">
                            <div>
                                <img src={this.state.user?.image} alt="/icons/account_circle.svg"/>
                            </div>
                            <div className="User-Info-Data">
                                <h1>Welcome {this.state.user?.username}</h1>
                                <label>Its your profile</label>
                            </div>
                        </div>
                        <div className="Links">
                            <div className="Link-Block">
                                <h4>User info</h4>
                                <hr/>
                                <label>Email: {this.state.user?.email}</label>
                                <label>Phone: {this.state.user?.phoneNumber == "" ? "-" : this.state.user?.phoneNumber}</label>
                            </div>
                            <div className="Link-Block">
                                <h4>Actions</h4>
                                <hr/>
                                <Button className="btn btn-outline-dark">Edit</Button>
                                <Button className="btn btn-outline-dark">Delete</Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}