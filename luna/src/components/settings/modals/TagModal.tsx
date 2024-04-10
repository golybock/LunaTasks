import React from "react";
import {Button, Modal} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import ITagBlank from "../../../models/card/blank/ITagBlank";
import TagProvider from "../../../provider/card/tagProvider";

interface IProps {
    closeModal: Function,
}

interface IState {
    tagBlank: ITagBlank
}

export default class TagModal extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            tagBlank: {name: "", hexColor: "", workspaceId: localStorage.getItem("workspaceId")!}
        }
    }

    headerChanged(value: string) {
        if (this.state.tagBlank) {
            this.setState({
                tagBlank: {
                    ...this.state.tagBlank,
                    name: value
                }
            })
        }
    }

    colorChanged(value: string) {
        if (this.state.tagBlank) {
            this.setState({
                tagBlank: {
                    ...this.state.tagBlank,
                    hexColor: value
                }
            })
        }
    }

    async saveTag() {
        const res = await TagProvider.createTag(this.state.tagBlank);

        if(res){
            this.props.closeModal();
        }
    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Create tag</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <Form>
                        <Form.Label>Наименование</Form.Label>
                        <Form.Control value={this.state.tagBlank.name}
                                      onChange={(e) => this.headerChanged(e.target.value)}/>

                        <Form.Label>Цвет</Form.Label>
                        <Form.Control type="color"
                                      value={this.state.tagBlank.hexColor}
                                      onChange={(e) => this.colorChanged(e.target.value)}/>

                    </Form>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn btn-outline-dark" onClick={() => this.saveTag()}>Save</Button>
                    <Button className="btn btn-outline-dark" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}