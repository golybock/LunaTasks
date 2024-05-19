import React from "react";
import {Button, Modal} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import IPageBlank from "../../../models/page/IPageBlank";
import PageProvider from "../../../provider/page/pageProvider";
import IPageView from "../../../models/page/IPageView";
import "./PageModal.css";

interface IProps {
    closeModal: Function,
    pageId: string | null
}

interface IState {
    pageBlank?: IPageBlank,
    pageView? : IPageView,
    isLoading: boolean
}

export default class PageModal extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            pageBlank: undefined,
            pageView: undefined,
            isLoading: true
        }
    }

    async componentDidMount() {
        if (this.props.pageId != null) {
            const pageView = await PageProvider.getPage(this.props.pageId);

            if (pageView != null) {
                const pageBlank = this.fromPageView(pageView);
                this.setState({pageBlank: pageBlank});
                this.setState({pageView: pageView});

                this.setState({isLoading: false});
            }
        } else {
            this.setState({pageBlank: this.getEmptyBlank()});

            this.setState({isLoading: false});
        }
    }

    fromPageView(pageView: IPageView): IPageBlank {
        return {
            name: pageView.name,
            description: pageView.description,
            workspaceId: localStorage.getItem("workspaceId") ?? "",
            headerImage: pageView.headerImage
        };
    }

    getEmptyBlank() : IPageBlank{
        return {
            name: "",
            description: "",
            workspaceId: localStorage.getItem("workspaceId") ?? "",
            headerImage: ""
        };
    }

    headerChanged(value: string) {
        if (this.state.pageBlank) {
            this.setState({
                pageBlank: {
                    ...this.state.pageBlank,
                    name: value
                }
            })
        }
    }

    descriptionChanged(value: string) {
        if (this.state.pageBlank) {
            this.setState({
                pageBlank: {
                    ...this.state.pageBlank,
                    description: value
                }
            })
        }
    }

    imageChanged(value: string) {
        if (this.state.pageBlank) {
            this.setState({
                pageBlank: {
                    ...this.state.pageBlank,
                    headerImage: value
                }
            })
        }
    }

    async savePage() {
        if (this.props.pageId != null) {
            const res = await PageProvider.updatePage(this.props.pageId, this.state.pageBlank!);

            if(res){
                this.props.closeModal();
            }
            else{
                //error
            }
        } else {
            const res = await PageProvider.createPage(this.state.pageBlank!);

            if(res){
                this.props.closeModal();
            }
            else{
                //error
            }
        }
    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()} data-bs-theme="dark">
                <Modal.Header closeButton className="Modal-Header">
                    <Modal.Title>Create page</Modal.Title>
                </Modal.Header>

                <Modal.Body className="Modal-Content">
                    <Form>
                        <Form.Label>Наименование</Form.Label>
                        <Form.Control value={this.state.pageBlank?.name}
                                      onChange={(e) => this.headerChanged(e.target.value)}/>

                        <Form.Label>Описание</Form.Label>
                        <Form.Control type="text"
                                      value={this.state.pageBlank?.description}
                                      onChange={(e) => this.descriptionChanged(e.target.value)}/>

                        <Form.Label>Изображение</Form.Label>
                        <Form.Control type="text"
                                      value={this.state.pageBlank?.headerImage}
                                      onChange={(e) => this.imageChanged(e.target.value)}/>

                        {this.state.pageBlank?.headerImage && (
                            <div>
                                <Form.Label>Preview</Form.Label>
                                <img src={this.state.pageBlank?.headerImage}/>
                            </div>
                        )}

                    </Form>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn Primary-Button" onClick={() => this.savePage()}>Save</Button>
                    <Button className="btn Primary-Button" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}