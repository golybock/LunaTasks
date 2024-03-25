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

interface IProps {

}

interface IState {
    workspace? : IWorkspaceView,
    tags: TagView[],
    statuses: IStatusView[],
    types: TypeView[],
    showTypeModal: boolean,
    showStatusModal:  boolean
    showTagModal: boolean
}

export default class About extends React.Component<IProps, IState> {

    headerUrl ="http://localhost:7005/woodcuts_14.jpg";

    constructor(props: IProps) {
        super(props);

        this.state = {
            workspace: undefined,
            tags: [],
            statuses: [],
            types: [],
            showTypeModal: false,
            showStatusModal: false,
            showTagModal: false
        }
    }

    showTypeModal(){
        this.setState({showTypeModal: true})
    }

    async closeTypeModal(){
        this.setState({showTypeModal: false})

        const types = await TypeProvider.getTypes();
        this.setState({types: types});
    }

    showTagModal(){
        this.setState({showTagModal: true})
    }

    async closeTagModal(){
        this.setState({showTagModal: false})

        const tags = await TagProvider.getTags();
        this.setState({tags: tags});
    }

    showStatusModal(){
        this.setState({showStatusModal: true})
    }

    async closeStatusModal(){
        this.setState({showStatusModal: false})

        const statuses = await StatusProvider.getStatuses();
        this.setState({statuses: statuses})
    }

    async componentDidMount() {

        const workspace = await WorkspaceProvider.getCurrentWorkspace();

        if(workspace != null){
            this.setState({workspace: workspace});
        }

        const tags = await TagProvider.getTags();
        this.setState({tags: tags});

        const types = await TypeProvider.getTypes();
        this.setState({types: types});

        const statuses = await StatusProvider.getStatuses();
        this.setState({statuses: statuses})
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
                            <div>
                                <h1>Workspace info</h1>
                                <label></label>
                            </div>
                        </div>
                        <div className="Items">
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Tags</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showTagModal()}>+</button>
                                </div>
                                <hr/>
                                {this.state.tags.length > 0 && (
                                    this.state.tags.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.name}</label>
                                            <Form.Control type="color" value={item.color}/>
                                        </div>)
                                    })
                                )}
                                {this.state.tags.length == 0 && (
                                    <div>No elements</div>
                                )}
                            </div>
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Types</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showTypeModal()}>+</button>
                                </div>
                                <hr/>
                                {this.state.types.length > 0 && (
                                    this.state.types.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.name}</label>
                                            <Form.Control type="color" value={item.color}/>
                                        </div>)
                                    })
                                )}
                                {this.state.types.length == 0 && (
                                    <div>No elements</div>
                                )}
                            </div>
                            <div className="Item-Block">
                                <div className="Item-Header">
                                    <h4>Statuses</h4>
                                    <button className="btn btn-outline-dark" onClick={() => this.showStatusModal()}>+</button>
                                </div>
                                <hr/>
                                {this.state.statuses.length > 0 && (
                                    this.state.statuses.map((item) => {
                                        return (<div className="Item" key={item.id}>
                                            <label>{item.name}</label>
                                            <Form.Control type="color" value={item.color}/>
                                        </div>)
                                    })
                                )}
                                {this.state.statuses.length == 0 && (
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

            </div>
        );
    }
}