import React from "react";
import {Button, Modal} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import "./WorkspaceModal.css";
import IWorkspaceBlank from "../../../models/workspace/IWorkspaceBlank";
import WorkspaceProvider from "../../../provider/workspace/workspaceProvider";

interface IProps {
    closeModal: Function,
}

interface IState {
    workspaceBlank?: IWorkspaceBlank
}

export default class WorkspaceModal extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            workspaceBlank: undefined,
        }
    }

    async componentDidMount() {
        this.setState({workspaceBlank: this.getEmptyBlank()});

    }

    getEmptyBlank() : IWorkspaceBlank{
        return {
            name: ""
        };
    }

    headerChanged(value: string) {
        if (this.state.workspaceBlank) {
            this.setState({
                workspaceBlank: {
                    ...this.state.workspaceBlank,
                    name: value
                }
            })
        }
    }

    async saveWorkspace() {
        const res = await WorkspaceProvider.createWorkspace(this.state.workspaceBlank!);

        if(res){
            this.props.closeModal();
            window.location.reload()
        }
        else{
            //error
        }
    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()} data-bs-theme="dark" className="Workspace-Modal">
                <Modal.Header closeButton>
                    <Modal.Title>Create workspace</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <Form>
                        <Form.Label>Name</Form.Label>
                        <Form.Control value={this.state.workspaceBlank?.name}
                                      onChange={(e) => this.headerChanged(e.target.value)}/>

                    </Form>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn Primary-Button" onClick={() => this.saveWorkspace()}>Save</Button>
                    <Button className="btn Primary-Button" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}