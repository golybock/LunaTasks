import React from "react";
import {useParams} from "react-router";
import {Button} from "react-bootstrap";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import IWorkspaceView from "../../models/workspace/IWorkspaceView";
import "./InviteWorkspacePage.css";

export default function InviteWorkspacePage() {
    const {workspaceId} = useParams();

    return (
        <InviteWorkspacePageContent workspaceId={workspaceId ?? ""}/>
    )
}

interface IProps {
    workspaceId: string
}

interface IState {
    info: string,
    workspace?: IWorkspaceView | null
}

class InviteWorkspacePageContent extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            info: ""
        }
    }

    async componentDidMount() {
        const workspace = await WorkspaceProvider.getWorkspace(this.props.workspaceId);

        this.setState({workspace: workspace});
    }

    async joinWorkspace() {
        const res = await WorkspaceProvider.joinToWorkspace(this.props.workspaceId);

        this.setState({info: res});
    }

    goToHomePage(){
        window.location.replace("/");
    }

    render() {
        return (
            <div>
                {this.state.workspace && (
                    <div className="Invite-Content">
                        <h2>{this.state.workspace?.name}</h2>
                        {!this.state.info && (
                            <div className="Invite-Accept">
                                <span>Хотите присоедениться к этому пространству?</span>
                                <Button onClick={() => this.joinWorkspace()}>Join</Button>
                            </div>
                        )}
                        {this.state.info && (
                            <div className="Toolbar-Items">
                                <label>{this.state.info}</label>
                                <Button className="btn btn-light btn-outline-secondary Outline-Button" onClick={this.goToHomePage}>Назад</Button>
                            </div>
                        )}
                    </div>
                )}
                {!this.state.workspace && (
                    <div>
                        <div className="Toolbar-Items">
                            <label>Invalid link"</label>
                            <Button className="btn btn-light btn-outline-secondary Outline-Button" onClick={this.goToHomePage}>Назад</Button>
                        </div>
                    </div>
                )}
            </div>
        );
    }
}