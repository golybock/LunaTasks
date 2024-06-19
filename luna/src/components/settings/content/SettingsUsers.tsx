import React from "react";
import "./SettingsUsers.css";
import IWorkspaceView from "../../../models/workspace/IWorkspaceView";
import IUserView from "../../../models/user/IUserView";
import NotificationManager from "../../../tools/NotificationManager";
import WorkspaceProvider from "../../../provider/workspace/workspaceProvider";
import {Button} from "react-bootstrap";

interface IProps {

}

interface IState {
    workspace?: IWorkspaceView,
    users: IUserView[],
}

export default class SettingsUsers extends React.Component<IProps, IState> {


    constructor(props: IProps) {
        super(props);

        this.state = {
            workspace: undefined,
            users: []
        }
    }

    async copyInviteLink() {
        const link = "http://localhost:3000/inviteWorkspace/" + this.state.workspace?.id;

        await navigator.clipboard.writeText(link);

        NotificationManager.makeSuccess("Link copied to clipboard!")
    }

    async componentDidMount() {

        const workspace = await WorkspaceProvider.getCurrentWorkspace();

        if (workspace != null) {
            this.setState({workspace: workspace});
        }

        const users = await WorkspaceProvider.getWorkspaceUsers();
        this.setState({users: users});
    }

    async deleteUser(id: string) {
        const res = await WorkspaceProvider.deleteUserFromWorkspace(id);

        if (res == true) {
            const users = await WorkspaceProvider.getWorkspaceUsers();
            this.setState({users: users});
        } else {
            NotificationManager.makeError(res.toString())
        }
    }

    render() {
        return (
            <div className="Settings-Users">
                <div className="Item-Block" id="users">
                    <div className="Item-Header">
                        <h4>Пользователи</h4>
                        <Button className="btn Outline-Button" onClick={() => this.copyInviteLink()}>+
                        </Button>
                    </div>
                    <hr/>
                    {this.state.users ?
                        (
                            this.state.users.map((item) => {
                                return (<div className="Item" key={item.id}>
                                    <label>{item.username}</label>
                                    <div className="row">
                                        <button className="btn Outline-Button"
                                                onClick={() => this.deleteUser(item.id)}>-
                                        </button>
                                    </div>
                                </div>)
                            })
                        ) :
                        (<div>Нет элементов</div>)
                    }
                </div>
            </div>
        );
    }
}
