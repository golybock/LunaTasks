import React from "react";
import "./EditPageModal.css";
import {Button, Modal} from "react-bootstrap";
import CardBlank from "../../models/card/blank/cardBlank";
import ICardBlank from "../../models/card/blank/cardBlank";
import ICardView from "../../models/card/view/cardView";
import CardProvider from "../../provider/card/cardProvider";
import Form from "react-bootstrap/Form";
import IOption from "../../models/tools/IOption";
import AsyncSelect from "react-select/async";
import {MultiValue, SingleValue} from "react-select";

interface IProps {
    closeModal: Function,
    cardId: string | null,
    pageId: string
}

interface IState {
    cardBlank?: CardBlank,
    cardView?: ICardView,
    selectedType?: IOption,
    selectedTags: IOption[],
    selectedStatus?: IOption,
    selectedUsers: IOption[]
}

export default class EditPageModal extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            cardBlank: undefined,
            cardView: undefined,
            selectedType: undefined,
            selectedStatus: undefined,
            selectedUsers: [],
            selectedTags: []
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
            this.setState({cardBlank: this.getEmptyBlank()});
        }
    }


    fromCardView(cardView: ICardView): ICardBlank {
        return {
            header: cardView.header,
            content: cardView.content,
            description: cardView.description,
            cardTypeId: cardView.cardType.id,
            pageId: this.props.pageId,
            deadline: cardView.deadline,
            previousCardId: cardView.previousCard?.id ?? null
        };
    }

    getEmptyBlank() : ICardBlank{
        return {
            header: "",
            content: "",
            description: "",
            cardTypeId: "",
            pageId: this.props.pageId,
            deadline: null,
            previousCardId: null
        };
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

    typeSelected(e: SingleValue<IOption>) {
        this.setState({selectedType: (e as IOption)})
    }

    tagSelected(e: MultiValue<IOption>) {
        this.setState({selectedTags: e.map(o => o)})
    }

    async getTypes() {
        return await CardProvider.getTypes();
    }

    async getTags() {
        return await CardProvider.getTags();
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

                        <Form.Label>Тип</Form.Label>
                        <AsyncSelect isMulti={false}
                                     cacheOptions
                                     defaultOptions
                                     value={this.state.selectedType}
                                     loadOptions={this.getTypes}
                                     onChange={(e) => this.typeSelected(e)}
                        />

                        <Form.Label>Теги</Form.Label>
                        <AsyncSelect isMulti={true}
                                     cacheOptions
                                     defaultOptions
                                     value={this.state.selectedTags}
                                     loadOptions={this.getTags}
                                     onChange={(e) => this.tagSelected(e)}
                        />
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