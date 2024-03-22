import React from "react";
import {Button, Modal} from "react-bootstrap";
import {Guid} from "guid-typescript";
import CardBlank from "../../models/card/blank/cardBlank";
import CardView from "../../models/card/view/cardView";
import CardProvider from "../../provider/card/cardProvider";
import "./EditPageModal.css";
import Form from "react-bootstrap/Form";

interface IProps {
    closeModal: Function,
    cardId: string | null,
    pageId: string
}

interface IState {
    cardBlank?: CardBlank,
    cardView?: CardView
}

export default class EditPageModal extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            cardBlank: undefined,
            cardView: undefined
        }
    }

    async componentDidMount() {
        if (this.props.cardId != null) {
            const cardView = await CardProvider.getCard(this.props.cardId);

            if (cardView != null) {
                const cardBlank = this.fromCardView(cardView);
                this.setState({cardBlank: cardBlank});
                this.setState({cardView: cardView});
            }
        } else {
            this.setState({cardBlank: new CardBlank("", "", "", "", this.props.pageId, null, null)});
        }
    }


    fromCardView(cardView: CardView): CardBlank {
        return new CardBlank(
            cardView.header,
            cardView.content,
            cardView.description,
            cardView.cardType.id,
            this.props.pageId,
            cardView.deadline,
            cardView.previousCard?.id ?? null
        );
    }

    headerChanged(value: string) {
        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    header: value ?? ""
                }
            })
        }
    }

    contentChanged(value: string) {
        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    content: value ?? ""
                }
            })
        }
    }

    descriptionChanged(value: string) {
        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    description: value ?? ""
                }
            })
        }
    }

    deadlineChanged(value: string) {
        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    deadline: value
                }
            })
        }
    }

    async saveCard() {
        if (this.props.cardId != null) {
            const res = await CardProvider.updateCard(this.props.cardId, this.state.cardBlank!);
        } else {
            const res = await CardProvider.createCard(this.state.cardBlank!);
        }
    }

    render() {
        return (
            <Modal show onHide={() => this.props.closeModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{this.props.cardId == null ? "Create task" : "Edit task"}</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <Form>
                        <Form.Label>Заголовок</Form.Label>
                        <Form.Control value={this.state.cardBlank?.header || ""}
                                      onChange={(e) => this.headerChanged(e.target.value)}/>
                        <Form.Label>Описание</Form.Label>
                        <Form.Control value={this.state.cardBlank?.description || ""}
                                      onChange={(e) => this.descriptionChanged(e.target.value)}/>
                        <Form.Label>Текст задачи</Form.Label>
                        <Form.Control value={this.state.cardBlank?.content || ""}
                                      onChange={(e) => this.contentChanged(e.target.value)}/>
                        <Form.Label>Дедлайн</Form.Label>
                        <Form.Control value={this.state.cardBlank?.deadline || ""}
                                      type="date"
                                      onChange={(e) => this.deadlineChanged(e.target.value)}/>
                    </Form>
                </Modal.Body>

                <Modal.Footer className="Modal-Footer">
                    <Button className="btn btn-outline-dark" onClick={() => this.saveCard()}>Save</Button>
                    <Button className="btn btn-outline-dark" onClick={() => this.props.closeModal()}>Cancel</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}