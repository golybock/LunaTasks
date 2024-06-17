import React from "react";
import {Button, Modal} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import ITypeBlank from "../../../models/card/blank/ITypeBlank";
import TypeProvider from "../../../provider/card/typeProvider";
import "./TypeModal.css";

interface IProps {
    closeModal: Function,
}

interface IState {
    typeBlank: ITypeBlank
}

export default class TypeModal extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            typeBlank: {name: "", hexColor: "", workspaceId: localStorage.getItem("workspaceId")!}
        }
    }

    headerChanged(value: string) {
        if (this.state.typeBlank) {
            this.setState({
                typeBlank: {
                    ...this.state.typeBlank,
                    name: value
                }
            })
        }
    }

    colorChanged(value: string) {
        if (this.state.typeBlank) {
            this.setState({
                typeBlank: {
                    ...this.state.typeBlank,
                    hexColor: value
                }
            })
        }
    }

    async saveType() {
        const res = await TypeProvider.createType(this.state.typeBlank);

        if(res){
            this.props.closeModal();
        }
    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()}>
                <Modal.Header closeButton className="Modal-Header">
                    <Modal.Title>Create type</Modal.Title>
                </Modal.Header>

                <Modal.Body className="Modal-Content">
                    <Form>
                        <Form.Label>Name</Form.Label>
                        <Form.Control value={this.state.typeBlank.name}
                                      onChange={(e) => this.headerChanged(e.target.value)}/>

                        <Form.Label>Color</Form.Label>
                        <Form.Control type="color"
                                      value={this.state.typeBlank.hexColor}
                                      onChange={(e) => this.colorChanged(e.target.value)}/>

                    </Form>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn Primary-Button" onClick={() => this.saveType()}>Save</Button>
                    <Button className="btn Primary-Button" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}