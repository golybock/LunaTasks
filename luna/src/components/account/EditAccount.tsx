﻿import React from "react";
import IUserView from "../../models/user/IUserView";
import UserProvider from "../../provider/user/userProvider";
import "./Account.css"
import {Button} from "react-bootstrap";
import ImageManager from "../../tools/ImageManager";
import Form from "react-bootstrap/Form";
import "./EditAccount.css";
import SelectImageModal from "./SelectImageModal";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IUserBlank from "../../models/user/IUserBlank";

interface IProps {

}

interface IState {
    user: IUserView | null,
    userBlank: IUserBlank | null,
    showModal: boolean
}

export default class EditAccount extends React.Component<IProps, IState> {

    headerUrl = ImageManager.getImageSrc("woodcuts_14.jpg");

    constructor(props: IProps) {
        super(props);

        this.state = {
            user: null,
            userBlank: null,
            showModal: false
        }
    }

    async componentDidMount() {
        const user = await UserProvider.getMe();

        this.setState({user: user});

        this.setState({userBlank: this.getUserBlank()});
    }

    getUserBlank(): IUserBlank{
        return {
            email: this.state.user?.email ?? "",
            image: this.state.user?.image ?? "",
            phoneNumber: this.state.user?.phoneNumber ?? "",
            username: this.state.user?.username ?? ""
        }
    }

    changeEmail(value: string) {

    }

    changePhone(value: string) {

    }

    showModal() {
        this.setState({showModal: true})
    }

    hideModal() {
        this.setState({showModal: false})
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
                            <div onClick={() => {
                                this.showModal()
                            }}>
                                <img src={this.state.userBlank?.image ?? "/icons/account_circle.svg"} alt=""/>
                            </div>
                            <div className="User-Info-Data">
                                <h1>Welcome {this.state.userBlank?.username}</h1>
                                <label>Its your profile</label>
                            </div>
                        </div>
                        <hr/>
                        <div className="Data">
                            <div className="Data-Block">
                                <label>Username</label>
                                <Form.Control value={this.state.userBlank?.username} disabled/>
                                <label>Email</label>
                                <Form.Control value={this.state.userBlank?.email}
                                              onChange={(e) => this.changeEmail(e.target.value)}/>
                                <Form.Check className="CheckBox" checked={this.state.user?.emailConfirmed} disabled
                                            label="Emal confirmed"/>
                            </div>
                            <div className="Data-Block">
                                <label>Phone</label>
                                <Form.Control value={this.state.userBlank?.phoneNumber}
                                              onChange={(e) => this.changePhone(e.target.value)}/>
                                <label>Register date</label>
                                <Form.Control value={this.state.user?.createdTimestamp} type="date"/>
                            </div>
                        </div>
                        <Button className="btn Primary-Button" onClick={async () => {
                            await UserProvider.updateUser(this.state.userBlank!);
                            // window.location.assign("/account")
                        }}>Save</Button>
                    </div>
                </div>
                {this.state.showModal && (
                    <SelectImageModal closeModal={() => this.hideModal()}
                                      imagePath={this.state.userBlank?.image ?? "/icons/account_circle.svg"}
                                      changeImage={(image: string) => {
                                          if (this.state.userBlank) {
                                              this.setState({
                                                  userBlank: {
                                                      ...this.state.userBlank,
                                                      image: image
                                                  }
                                              })
                                          }
                                      }}/>
                )}

            </div>
        );
    }
}