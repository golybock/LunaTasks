﻿import React from "react";
import {Button, Modal} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import ITypeBlank from "../../../models/card/blank/statusBlank";
import TypeProvider from "../../../provider/card/typeProvider";
import IStatusBlank from "../../../models/card/blank/statusBlank";
import StatusProvider from "../../../provider/card/statusProvider";

interface IProps {
    closeModal: Function,
}

interface IState {
    statusBlank: IStatusBlank
}

export default class StatusModal extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            statusBlank: {name: "", hexColor: "", workspaceId: localStorage.getItem("workspaceId")!}
        }
    }

    headerChanged(value: string) {
        if (this.state.statusBlank) {
            this.setState({
                statusBlank: {
                    ...this.state.statusBlank,
                    name: value
                }
            })
        }
    }

    colorChanged(value: string) {
        if (this.state.statusBlank) {
            this.setState({
                statusBlank: {
                    ...this.state.statusBlank,
                    hexColor: value
                }
            })
        }
    }

    async saveStatus() {
        const res = await StatusProvider.createStatus(this.state.statusBlank);

        if(res){
            this.props.closeModal();
        }
    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Create status</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <Form>
                        <Form.Label>Наименование</Form.Label>
                        <Form.Control value={this.state.statusBlank.name}
                                      onChange={(e) => this.headerChanged(e.target.value)}/>

                        <Form.Label>Цвет</Form.Label>
                        <Form.Control type="color"
                                      value={this.state.statusBlank.hexColor}
                                      onChange={(e) => this.colorChanged(e.target.value)}/>

                    </Form>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn btn-outline-dark" onClick={() => this.saveStatus()}>Save</Button>
                    <Button className="btn btn-outline-dark" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}