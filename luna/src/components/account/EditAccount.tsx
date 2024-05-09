import React from "react";
import IUserView from "../../models/user/IUserView";
import UserProvider from "../../provider/user/userProvider";
import "./Account.css"
import {Button} from "react-bootstrap";
import WorkspaceModal from "./modals/WorkspaceModal";
import ImageManager from "../../tools/ImageManager";
import Form from "react-bootstrap/Form";

interface IProps {

}

interface IState {
    user: IUserView | null
}

export default class EditAccount extends React.Component<IProps, IState> {

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

    changeEmail(value: string) {

    }

    changePhone(value: string) {

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
                        </div>
                        <div className="Links">
                            <div className="Link-Block">
                                <label>Email</label>
                                <Form.Control value={this.state.user?.email}
                                              onChange={(e) => this.changeEmail(e.target.value)}/>
                                <label>Phone</label>
                                <Form.Control value={this.state.user?.phoneNumber}
                                              onChange={(e) => this.changePhone(e.target.value)}/>
                                <Button className="btn btn-outline-dark Outline-Button">Save</Button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        );
    }
}