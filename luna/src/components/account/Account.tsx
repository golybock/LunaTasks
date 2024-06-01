import React from "react";
import IUserView from "../../models/user/IUserView";
import UserProvider from "../../provider/user/userProvider";
import "./Account.css"
import {Button} from "react-bootstrap";
import WorkspaceModal from "./modals/WorkspaceModal";
import ImageManager from "../../tools/ImageManager";
import {NavLink} from "react-router-dom";
import SelectImageModal from "./SelectImageModal";

interface IProps {

}

interface IState {
    user: IUserView | null,

}

export default class Account extends React.Component<IProps, IState> {

    headerUrl = ImageManager.getImageSrc("woodcuts_14.jpg");

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
                                <img src={this.state.user?.image ?? "/icons/account_circle.svg"} alt=""/>
                            </div>
                            <div className="User-Info-Data">
                                <h1>Welcome {this.state.user?.username}</h1>
                                <label>Its your profile</label>
                            </div>
                            <Button className="btn Primary-Button" onClick={() => {
                                window.location.assign("/editAccount")
                            }}>
                                Edit
                            </Button>
                        </div>
                        <hr/>
                        <div className="Links">
                            <div className="Link-Block">
                                <h4>User info</h4>
                                <hr/>
                                <label>Email: {this.state.user?.email}</label>
                                <label>Phone: {this.state.user?.phoneNumber == "" ? "-" : this.state.user?.phoneNumber}</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}