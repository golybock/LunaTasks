import React from "react";
import IWorkspaceView from "../../models/workspace/workspaceView";
import TagView from "../../models/card/view/tagView";
import IStatusView from "../../models/card/view/statusView";
import TypeView from "../../models/card/view/typeView";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import TagProvider from "../../provider/card/tagProvider";
import TypeProvider from "../../provider/card/typeProvider";
import StatusProvider from "../../provider/card/statusProvider";
import "./About.css";
import Form from "react-bootstrap/Form";
import TypeModal from "./modals/TypeModal";
import TagModal from "./modals/TagModal";
import StatusModal from "./modals/StatusModal";
import PageModal from "./modals/PageModal";
import {Button} from "react-bootstrap";
import {Await} from "react-router";
import IUserView from "../../models/user/userView";

interface IProps {

}

interface IState {
    workspace?: IWorkspaceView,
    tags: TagView[],
    statuses: IStatusView[],
    types: TypeView[],
    users: IUserView[],
    showTypeModal: boolean,
    showStatusModal: boolean
    showTagModal: boolean,
    showPageModal: boolean
}

export default class About extends React.Component<IProps, IState> {

    headerUrl = "http://localhost:7005/woodcuts_14.jpg";

    constructor(props: IProps) {
        super(props);

        this.state = {
            workspace: undefined,
            tags: [],
            statuses: [],
            types: [],
            users: [],
            showTypeModal: false,
            showStatusModal: false,
            showTagModal: false,
            showPageModal: false
        }
    }

    showTypeModal() {
        this.setState({showTypeModal: true})
    }

    async closeTypeModal() {
        this.setState({showTypeModal: false})

        const types = await TypeProvider.getTypes();
        this.setState({types: types});
    }

    showTagModal() {
        this.setState({showTagModal: true})
    }

    async closeTagModal() {
        this.setState({showTagModal: false})

        const tags = await TagProvider.getTags();
        this.setState({tags: tags});
    }

    showStatusModal() {
        this.setState({showStatusModal: true})
    }

    async closeStatusModal() {
        this.setState({showStatusModal: false})

        const statuses = await StatusProvider.getStatuses();
        this.setState({statuses: statuses})
    }

    showPageModal() {
        this.setState({showPageModal: true})
    }

    async copyInviteLink() {
        const link = "http://localhost:3000/inviteWorkspace/" + this.state.workspace?.id;

        await navigator.clipboard.writeText(link);
    }

    closePageModal() {
        this.setState({showPageModal: false})

        window.location.reload();
    }

    async componentDidMount() {

        const workspace = await WorkspaceProvider.getCurrentWorkspace();

        if (workspace != null) {
            this.setState({workspace: workspace});
        }

        const tags = await TagProvider.getTags();
        this.setState({tags: tags});

        const types = await TypeProvider.getTypes();
        this.setState({types: types});

        const statuses = await StatusProvider.getStatuses();
        this.setState({statuses: statuses})

        const users = await WorkspaceProvider.getWorkspaceUsers(localStorage.getItem("workspaceId") ?? "");
        this.setState({users: users});
    }

    async deleteStatus(id: string) {
        const res = await StatusProvider.deleteStatus(id);

        if (res) {
            const statuses = await StatusProvider.getStatuses();
            this.setState({statuses: statuses})
        } else {
            console.log("ошибка удаления")
        }
    }

    async deleteTag(id: string) {
        const res = await TagProvider.deleteTag(id);

        if (res) {
            const tags = await TagProvider.getTags();
            this.setState({tags: tags});
        } else {
            console.log("ошибка удаления")
        }
    }

    async deleteType(id: string) {
        const res = await TypeProvider.deleteType(id);

        if (res) {
            const types = await TypeProvider.getTypes();
            this.setState({types: types});
        } else {
            console.log("ошибка удаления")
        }
    }

    render() {
        return (
            <div>
                <div className="Header-Image-Container">
                    <img src={this.headerUrl} alt=""/>
                </div>
                <div className="About-Content">
                    <div>
                        <div className="Header">
                            <div className="Workspace-Header">
                                <h1>Workspace info</h1>
                                <div className="Workspace-Header-Toolbar">
                                    <Button className="btn btn-light btn-outline-secondary Outline-Button" onClick={() => this.showPageModal()}>Create page</Button>
                                    <Button className="btn btn-light btn-outline-secondary Outline-Button" onClick={() => this.copyInviteLink()}>Invite user</Button>
                                </div>
                            </div>
                        </div>
                        <div className="Items">
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Tags</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showTagModal()}>+
                                    </button>
                                </div>
                                <hr/>
                                {this.state.tags && (
                                    this.state.tags.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.name}</label>
                                            <div className="row">
                                                <Form.Control disabled type="color" value={item.color}/>
                                                <button className="btn btn-outline-dark"
                                                        onClick={() => this.deleteTag(item.id)}>-
                                                </button>
                                            </div>
                                        </div>)
                                    })
                                )}
                                {!this.state.tags && (
                                    <div>No elements</div>
                                )}
                            </div>
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Types</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showTypeModal()}>+
                                    </button>
                                </div>
                                <hr/>
                                {this.state.types && (
                                    this.state.types.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.name}</label>
                                            <div className="row">
                                                <Form.Control disabled type="color" value={item.color}/>
                                                <button className="btn btn-outline-dark"
                                                        onClick={() => this.deleteType(item.id)}>-
                                                </button>
                                            </div>
                                        </div>)
                                    })
                                )}
                                {!this.state.types && (
                                    <div>No elements</div>
                                )}
                            </div>
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Statuses</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showStatusModal()}>+
                                    </button>
                                </div>
                                <hr/>
                                {this.state.statuses.length && (
                                    this.state.statuses.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.name}</label>
                                            <div className="row">
                                                <Form.Control disabled type="color" value={item.color}/>
                                                <button className="btn btn-outline-dark"
                                                        onClick={() => this.deleteStatus(item.id)}>-
                                                </button>
                                            </div>
                                        </div>)
                                    })
                                )}
                                {!this.state.statuses && (
                                    <div>No elements</div>
                                )}
                            </div>
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Users</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showStatusModal()}>+
                                    </button>
                                </div>
                                <hr/>
                                {this.state.users && (
                                    this.state.users.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.username}</label>
                                            <div className="row">
                                                <button className="btn btn-outline-dark"
                                                        onClick={() => this.deleteStatus(item.id)}>-
                                                </button>
                                            </div>
                                        </div>)
                                    })
                                )}
                                {!this.state.users && (
                                    <div>No elements</div>
                                )}
                            </div>
                        </div>
                    </div>
                </div>

                {this.state.showTypeModal && (
                    <TypeModal closeModal={() => this.closeTypeModal()}/>
                )}

                {this.state.showTagModal && (
                    <TagModal closeModal={() => this.closeTagModal()}/>
                )}

                {this.state.showStatusModal && (
                    <StatusModal closeModal={() => this.closeStatusModal()}/>
                )}

                {this.state.showPageModal && (
                    <PageModal pageId={null} closeModal={() => this.closePageModal()}/>
                )}

            </div>
        );
    }
}