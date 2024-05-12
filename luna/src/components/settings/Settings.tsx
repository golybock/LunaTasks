import React from "react";
import IWorkspaceView from "../../models/workspace/IWorkspaceView";
import ITagView from "../../models/card/view/ITagView";
import IStatusView from "../../models/card/view/IStatusView";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import TypeView from "../../models/card/view/ITypeView";
import TagProvider from "../../provider/card/tagProvider";
import TypeProvider from "../../provider/card/typeProvider";
import StatusProvider from "../../provider/card/statusProvider";
import Form from "react-bootstrap/Form";
import TypeModal from "./modals/TypeModal";
import TagModal from "./modals/TagModal";
import StatusModal from "./modals/StatusModal";
import PageModal from "./modals/PageModal";
import {Button} from "react-bootstrap";
import IUserView from "../../models/user/IUserView";
import NotificationManager from "../../tools/NotificationManager";
import "./Settings.css";
import SettingsModal from "./SettingsModal";

interface IProps {

}

interface IState {
    workspace?: IWorkspaceView,
    tags: ITagView[],
    statuses: IStatusView[],
    types: TypeView[],
    users: IUserView[],
    showTypeModal: boolean,
    showStatusModal: boolean
    showTagModal: boolean,
    showPageModal: boolean
}

export default class Settings extends React.Component<IProps, IState> {

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

        NotificationManager.makeSuccess("Link copied to clipboard!")
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

        const users = await WorkspaceProvider.getWorkspaceUsers();
        this.setState({users: users});
    }

    async deleteStatus(id: string) {
        const res = await StatusProvider.deleteStatus(id);

        if (res) {
            const statuses = await StatusProvider.getStatuses();
            this.setState({statuses: statuses})
        } else {
            NotificationManager.makeError("Error")
        }
    }

    async deleteUser(id: string) {
        const res = await WorkspaceProvider.deleteUserFromWorkspace(id);

        if (res) {
            const users = await WorkspaceProvider.getWorkspaceUsers();
            this.setState({users: users});
        } else {
            NotificationManager.makeError("Error")
        }
    }

    async deleteTag(id: string) {
        const res = await TagProvider.deleteTag(id);

        if (res) {
            const tags = await TagProvider.getTags();
            this.setState({tags: tags});
        } else {
            NotificationManager.makeError("Error")
        }
    }

    async deleteType(id: string) {
        const res = await TypeProvider.deleteType(id);

        if (res) {
            const types = await TypeProvider.getTypes();
            this.setState({types: types});
        } else {
            NotificationManager.makeError("Error")
        }
    }

// {/*<div>*/}
// {/*    <div className="Header-Image-Container">*/}
// {/*        <img src={this.headerUrl} alt=""/>*/}
// {/*    </div>*/}
// {/*    <div className="About-Content">*/}
// {/*        <div>*/}
// {/*            <div className="Header">*/}
// {/*                <div className="Workspace-Header">*/}
// {/*                    <h1>Settings</h1>*/}
// {/*                    <div className="Workspace-Header-Toolbar">*/}
// {/*                        <a href="#tags">Tags</a>*/}
// {/*                        <a href="#types">Types</a>*/}
// {/*                        <a href="#statuses">Statuses</a>*/}
// {/*                        <a href="#users">Users</a>*/}
// {/*                        <a href="#pages">Pages</a>*/}
// {/*                    </div>*/}
// {/*                </div>*/}
// {/*            </div>*/}
// {/*            <div className="Items">*/}
// {/*                <div className="Item-Block" id="tags">*/}
// {/*                    <div className="Item-Header">*/}
// {/*                        <h4>Tags</h4>*/}
// {/*                        <button className="btn Outline-Button" onClick={() => this.showTagModal()}>+*/}
// {/*                        </button>*/}
// {/*                    </div>*/}
// {/*                    <hr/>*/}
// {/*                    {this.state.tags ?*/}
// {/*                        (*/}
// {/*                            this.state.tags.map((item) => {*/}
// {/*                                return (<div className="Item" key={item.id}>*/}
// {/*                                    <label>{item.name}</label>*/}
// {/*                                    <div className="row">*/}
// {/*                                        <Form.Control disabled type="color" value={item.color}/>*/}
// {/*                                        <button className="btn Outline-Button"*/}
// {/*                                                onClick={() => this.deleteTag(item.id)}>-*/}
// {/*                                        </button>*/}
// {/*                                    </div>*/}
// {/*                                </div>)*/}
// {/*                            })*/}
// {/*                        ) :*/}
// {/*                        <div>No elements</div>*/}
// {/*                    }*/}
// {/*                </div>*/}
// {/*                <div className="Item-Block" id="types">*/}
// {/*                    <div className="Item-Header">*/}
// {/*                        <h4>Types</h4>*/}
// {/*                        <Button className="btn Outline-Button" onClick={() => this.showTypeModal()}>+*/}
// {/*                        </Button>*/}
// {/*                    </div>*/}
// {/*                    <hr/>*/}
// {/*                    {this.state.types ?*/}
// {/*                        (*/}
// {/*                            this.state.types.map((item) => {*/}
// {/*                                return (<div className="Item" key={item.id}>*/}
// {/*                                    <label>{item.name}</label>*/}
// {/*                                    <div className="row">*/}
// {/*                                        <Form.Control disabled type="color" value={item.color}/>*/}
// {/*                                        <button className="btn Outline-Button"*/}
// {/*                                                onClick={() => this.deleteType(item.id)}>-*/}
// {/*                                        </button>*/}
// {/*                                    </div>*/}
// {/*                                </div>)*/}
// {/*                            })*/}
// {/*                        )*/}
// {/*                        :*/}
// {/*                        (<div>No elements</div>)*/}
// {/*                    }*/}
// {/*                </div>*/}
// {/*                <div className="Item-Block" id="sttuses">*/}
// {/*                    <div className="Item-Header">*/}
// {/*                        <h4>Statuses</h4>*/}
// {/*                        <button className="btn Outline-Button" onClick={() => this.showStatusModal()}>+*/}
// {/*                        </button>*/}
// {/*                    </div>*/}
// {/*                    <hr/>*/}
// {/*                    {this.state.statuses ?*/}
// {/*                        (*/}
// {/*                            this.state.statuses.map((item) => {*/}
// {/*                                return (<div className="Item" key={item.id}>*/}
// {/*                                    <label>{item.name}</label>*/}
// {/*                                    <div className="row">*/}
// {/*                                        <Form.Control disabled type="color" value={item.color}/>*/}
// {/*                                        <button className="btn Outline-Button"*/}
// {/*                                                onClick={() => this.deleteStatus(item.id)}>-*/}
// {/*                                        </button>*/}
// {/*                                    </div>*/}
// {/*                                </div>)*/}
// {/*                            })*/}
// {/*                        )*/}
// {/*                        :*/}
// {/*                        (<div>No elements</div>)}*/}
// {/*                </div>*/}
// {/*                <div className="Item-Block" id="users">*/}
// {/*                    <div className="Item-Header">*/}
// {/*                        <h4>Users</h4>*/}
// {/*                        <Button className="btn Outline-Button" onClick={() => this.copyInviteLink()}>+*/}
// {/*                        </Button>*/}
// {/*                    </div>*/}
// {/*                    <hr/>*/}
// {/*                    {this.state.users ?*/}
// {/*                        (*/}
// {/*                            this.state.users.map((item) => {*/}
// {/*                                return (<div className="Item" key={item.id}>*/}
// {/*                                    <label>{item.username}</label>*/}
// {/*                                    <div className="row">*/}
// {/*                                        <button className="btn Outline-Button"*/}
// {/*                                                onClick={() => this.deleteUser(item.id)}>-*/}
// {/*                                        </button>*/}
// {/*                                    </div>*/}
// {/*                                </div>)*/}
// {/*                            })*/}
// {/*                        ) :*/}
// {/*                        (<div>No elements</div>)*/}
// {/*                    }*/}
// {/*                </div>*/}
// {/*                <div className="Item-Block" id="pages">*/}
// {/*                    <div className="Item-Header">*/}
// {/*                        <h4>Pages</h4>*/}
// {/*                        <button className="btn Outline-Button" onClick={() => this.showPageModal()}>+*/}
// {/*                        </button>*/}
// {/*                    </div>*/}
// {/*                    <hr/>*/}
// {/*                    /!*{this.state.statuses ?*!/*/}
// {/*                    /!*    (*!/*/}
// {/*                    /!*        this.state.statuses.map((item) => {*!/*/}
// {/*                    /!*            return (<div className="Item" key={item.id}>*!/*/}
// {/*                    /!*                <label>{item.name}</label>*!/*/}
// {/*                    /!*                <div className="row">*!/*/}
// {/*                    /!*                    <Form.Control disabled type="color" value={item.color}/>*!/*/}
// {/*                    /!*                    <button className="btn Outline-Button"*!/*/}
// {/*                    /!*                            onClick={() => this.deleteStatus(item.id)}>-*!/*/}
// {/*                    /!*                    </button>*!/*/}
// {/*                    /!*                </div>*!/*/}
// {/*                    /!*            </div>)*!/*/}
// {/*                    /!*        })*!/*/}
// {/*                    /!*    )*!/*/}
// {/*                    /!*    :*!/*/}
// {/*                    /!*    (<div>No elements</div>)}*!/*/}
// {/*                </div>*/}
// {/*            </div>*/}
// {/*        </div>*/}
// {/*    </div>*/}
//
// {/*    {this.state.showTypeModal && (*/}
// {/*        <TypeModal closeModal={() => this.closeTypeModal()}/>*/}
// {/*    )}*/}
//
// {/*    {this.state.showTagModal && (*/}
// {/*        <TagModal closeModal={() => this.closeTagModal()}/>*/}
// {/*    )}*/}
//
// {/*    {this.state.showStatusModal && (*/}
// {/*        <StatusModal closeModal={() => this.closeStatusModal()}/>*/}
// {/*    )}*/}
//
// {/*    {this.state.showPageModal && (*/}
// {/*        <PageModal pageId={null} closeModal={() => this.closePageModal()}/>*/}
// {/*    )}*/}
//
// {/*</div>*/}

    render() {
        return (
            <div>
                <SettingsModal show={true}/>
            </div>

        );
    }
}