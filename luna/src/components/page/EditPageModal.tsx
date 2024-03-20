import React from "react";
import {Button, Modal} from "react-bootstrap";

interface IProps {
    closeModal: Function
}

interface IState {
}

export default class EditPageModal extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Create task</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <></>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn btn-outline-dark">Save</Button>
                    <Button className="btn btn-outline-dark" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}