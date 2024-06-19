import React from "react";
import IUserView from "../../models/user/IUserView";
import UserProvider from "../../provider/user/userProvider";
import "./Account.css"
import {Button} from "react-bootstrap";
import ImageManager from "../../tools/ImageManager";
import {Checkbox, createTheme, FormControlLabel, ThemeProvider} from "@mui/material";


const darkTheme = createTheme({
    palette: {
        mode: 'dark',
    },
});

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
                <div className="Account-Content">
                    <div>
                        <div className="User-Info">
                            <div>
                                <img src={this.state.user?.image ?? "/icons/account_circle.svg"} alt=""/>
                            </div>
                            <div className="User-Info-Data">
                                <h1>Добро пожаловать {this.state.user?.username}</h1>
                                <label>Это ваш профиль</label>
                            </div>
                            <Button className="btn Primary-Button" onClick={() => {
                                window.location.assign("/editAccount")
                            }}>
                                Редактировать
                            </Button>
                        </div>
                        <div className="Links">
                            <div className="Link-Block">
                                <h4>Информация</h4>
                                <hr/>
                                <label>Почта: {this.state.user?.email}</label>
                                <label>Дата регистрации: {(new Date(this.state.user?.createdTimestamp ?? "")).toDateString()}</label>
                                <label>Номер телефона: {this.state.user?.phoneNumber == "" ? "-" : this.state.user?.phoneNumber}</label>
                                {/*<ThemeProvider theme={darkTheme}>*/}
                                {/*    <FormControlLabel control={<Checkbox checked={this.state.user?.emailConfirmed} disabled/>} label="Email confirmed" />*/}
                                {/*</ThemeProvider>*/}

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}