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
    showPageModal: boolean
}

export default class Settings extends React.Component<IProps, IState> {

    headerUrl = "http://localhost:7005/woodcuts_14.jpg";

    constructor(props: IProps) {
        super(props);

        this.state = {
            showPageModal: false
        }
    }



    showPageModal() {
        this.setState({showPageModal: true})
    }


    closePageModal() {
        this.setState({showPageModal: false})

        window.location.reload();
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
// {/*        </div>*/}
// {/*    </div>*/}
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