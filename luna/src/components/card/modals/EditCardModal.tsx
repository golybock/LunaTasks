import React from "react";
import "./EditCardModal.css";
import 'react-quill/dist/quill.snow.css';
import {Button, InputGroup, Modal} from "react-bootstrap";
import ICardBlank from "../../../models/card/blank/ICardBlank";
import ICardView from "../../../models/card/view/ICardView";
import CardProvider from "../../../provider/card/cardProvider";
import Form from "react-bootstrap/Form";
import IOption from "../../../models/tools/IOption";
import {MultiValue, SingleValue} from "react-select";
import UserProvider from "../../../provider/user/userProvider";
import TagProvider from "../../../provider/card/tagProvider";
import TypeProvider from "../../../provider/card/typeProvider";
import StatusProvider from "../../../provider/card/statusProvider";
import Loading from "../../notifications/Loading";
import ReactQuill from "react-quill";
import DarkAsyncSelect from "../../tools/DarkAsyncSelect";
import WorkspaceProvider from "../../../provider/workspace/workspaceProvider";
import {WorkspaceManager} from "../../../tools/WorkspaceManager";
import {Input} from "react-select/animated";
import NotificationManager from "../../../tools/NotificationManager";
import CommentProvider from "../../../provider/card/commentProvider";
import Message from "../Message";
import CommentChat from "../comments/CommentChat";
import {AuthWrapper} from "../../../auth/AuthWrapper";
import ICommentView from "../../../models/card/view/ICommentView";
import {Guid} from "guid-typescript";

interface IProps {
    closeModal: Function,
    cardId: string | null,
    pageId: string
}

interface IState {
    cardBlank?: ICardBlank,
    cardView?: ICardView,
    comments: ICommentView[],
    selectedType?: IOption,
    selectedTags: IOption[],
    selectedStatus?: IOption,
    selectedUsers: IOption[],
    isLoading: boolean,
    rteValue: string | null;
    commentText: string;
}

export default class EditCardModal extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            cardBlank: undefined,
            cardView: undefined,
            comments: [],
            selectedType: undefined,
            selectedStatus: undefined,
            selectedUsers: [],
            selectedTags: [],
            isLoading: true,
            rteValue: "",
            commentText: ""
        }
    }

    async componentDidMount() {
        if (this.props.cardId != null) {
            const cardView = await CardProvider.getCard(this.props.cardId);

            if (cardView != null) {
                const cardBlank = this.fromCardView(cardView);
                this.setState({cardBlank: cardBlank});
                this.setState({cardView: cardView});
                this.setState({rteValue: cardView.content})
                this.setState({comments: cardView.comments})

                this.setSelectedTags();
                this.setSelectedType();
                this.setSelectedStatus();
                this.setSelectedUsers();

                this.setState({isLoading: false});
            }
        } else {
            this.setState({cardBlank: this.getEmptyBlank()});

            this.setState({isLoading: false});
        }
    }

    setSelectedTags() {
        let tags = this.state.cardView?.cardTags;

        let arr: Array<{ label: string, value: string }> = []

        tags?.forEach(element => {
            let selected = {label: element.name, value: element.id};
            arr.push(selected);
        });

        this.setState({selectedTags: arr})
    }

    setSelectedUsers() {
        let users = this.state.cardView?.users;

        let arr: Array<{ label: string, value: string }> = []

        users?.forEach(element => {
            let selected = {label: element.username, value: element.id};
            arr.push(selected);
        });

        this.setState({selectedUsers: arr})
    }

    setSelectedType() {
        let type = this.state.cardView?.cardType;

        this.setState({selectedType: {label: type?.name ?? "", value: type?.id ?? ""}})
    }

    setSelectedStatus() {
        let status = this.state.cardView?.status;

        if (status) {
            this.setState({selectedStatus: {label: status.name, value: status.id}})
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
            previousCardId: cardView.previousCard?.id ?? null,
            userIds: cardView.users.map(u => u.id),
            tagIds: cardView.cardTags.map(t => t.id),
            statusId: cardView.status?.id ?? ""
        };
    }

    getEmptyBlank(): ICardBlank {
        return {
            header: "",
            content: "",
            description: "",
            cardTypeId: "",
            pageId: this.props.pageId,
            deadline: null,
            previousCardId: null,
            userIds: [],
            statusId: "",
            tagIds: []
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

            if (res) {
                this.props.closeModal();
            } else {
                //error
            }
        } else {
            const res = await CardProvider.createCard(this.state.cardBlank!);

            if (res) {
                this.props.closeModal();
            } else {
                //error
            }
        }
    }

    typeSelected(e: SingleValue<IOption>) {
        this.setState({selectedType: (e as IOption)})

        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    cardTypeId: e!.value
                }
            })
        }
    }

    statusSelected(e: SingleValue<IOption>) {
        this.setState({selectedStatus: (e as IOption)})

        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    statusId: e!.value
                }
            })
        }
    }

    tagSelected(e: MultiValue<IOption>) {
        this.setState({selectedTags: e.map(o => o)})

        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    tagIds: e.map(i => i.value)
                }
            })
        }
    }

    userSelected(e: MultiValue<IOption>) {
        this.setState({selectedUsers: e.map(o => o)})

        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    userIds: e.map(i => i.value)
                }
            })
        }
    }

    contentChanged(value: string) {
        this.setState({rteValue: value})

        if (this.state.cardBlank != undefined) {
            this.setState({
                cardBlank: {
                    ...this.state.cardBlank,
                    content: value
                }
            })
        }
    }

    async getTypes() {
        return await TypeProvider.getTypesOptions();
    }

    async getStatuses() {
        return await StatusProvider.getStatusesOptions();
    }

    async getUsers() {
        return await WorkspaceProvider.getWorkspaceUsersOptions(WorkspaceManager.getWorkspace()!);
    }

    async getTags() {
        return await TagProvider.getTagsOptions();
    }

    render() {
        // @ts-ignore
        return (
            <>
                {this.state.isLoading && (
                    <Loading/>
                )}
                {!this.state.isLoading && (
                    <Modal dialogClassName="Modal" show onHide={() => this.props.closeModal()}>

                        <Modal.Header closeButton>
                            <Modal.Title
                                className="Modal-Header">{this.props.cardId == null ? "Create task" : "Edit task"}</Modal.Title>
                        </Modal.Header>

                        <Modal.Body className="Modal-Content">
                            <Form>
                                <div className="Modal-Body">

                                    <div className="Modal-Body-Header">
                                        <div className="Modal-Body-Item">

                                            <Form.Label>Header</Form.Label>
                                            <Form.Control value={this.state.cardBlank?.header || ""}
                                                          onChange={(e) => this.headerChanged(e.target.value)}/>

                                            <Form.Label>Description</Form.Label>
                                            <Form.Control value={this.state.cardBlank?.description || ""}
                                                          onChange={(e) => this.descriptionChanged(e.target.value)}/>

                                            <Form.Label>Deadline</Form.Label>
                                            <Form.Control value={this.state.cardBlank?.deadline || ""}
                                                          type="date"
                                                          onChange={(e) => this.deadlineChanged(e.target.value)}/>

                                            <Form.Label>Type</Form.Label>
                                            <DarkAsyncSelect isMulti={false}
                                                             cacheOptions
                                                             defaultOptions
                                                             value={this.state.selectedType}
                                                             loadOptions={this.getTypes}
                                                             onChange={(e: SingleValue<IOption>) => this.typeSelected(e)}/>


                                        </div>
                                        <div className="Modal-Body-Item">
                                            <Form.Label>Status</Form.Label>
                                            <DarkAsyncSelect isMulti={false}
                                                             cacheOptions
                                                             defaultOptions
                                                             value={this.state.selectedStatus}
                                                             loadOptions={this.getStatuses}
                                                             onChange={(e: SingleValue<IOption>) => this.statusSelected(e)}/>


                                            <Form.Label>Tags</Form.Label>
                                            <DarkAsyncSelect isMulti={true}
                                                             cacheOptions
                                                             defaultOptions
                                                             value={this.state.selectedTags}
                                                             loadOptions={this.getTags}
                                                             onChange={(e: MultiValue<IOption>) => this.tagSelected(e)}/>

                                            <Form.Label>Users</Form.Label>
                                            <DarkAsyncSelect isMulti={true}
                                                             cacheOptions
                                                             defaultOptions
                                                             value={this.state.selectedUsers}
                                                             loadOptions={this.getUsers}
                                                             onChange={(e: MultiValue<IOption>) => this.userSelected(e)}/>
                                        </div>
                                        <div className="Modal-Body-Item">
                                            <Form.Label>Comments</Form.Label>
                                            <InputGroup className="mb-3">
                                                <Form.Control
                                                    placeholder="Введите текст комментария"
                                                    value={this.state.commentText}
                                                    onChange={(e) => {
                                                        this.setState({commentText: e.target.value})
                                                    }}
                                                />
                                                <InputGroup.Text onClick={async () => {

                                                    const commentBlank = {
                                                        cardId: this.state.cardView?.id ?? "",
                                                        comment: this.state.commentText,
                                                        attachmentUrl: ""
                                                    };

                                                    const res = await CommentProvider.createComment(commentBlank);

                                                    if(res){
                                                        NotificationManager.makeSuccess("Comment sent");

                                                        // load comments
                                                        this.state.cardView?.comments?.push({
                                                            userId: AuthWrapper.userId() ?? "",
                                                            comment: commentBlank.comment,
                                                            user: undefined,
                                                            id: Guid.create().toString(),
                                                            attachmentUrl: ""
                                                        })

                                                        this.setState({comments: this.state.cardView?.comments ?? []})

                                                        return;
                                                    }

                                                    NotificationManager.makeSuccess("Error");

                                                }}>{">"}</InputGroup.Text>
                                            </InputGroup>
                                            {this.state.comments ? (
                                                        <CommentChat comments={this.state.cardView?.comments ?? []}/>
                                                ) :
                                                (
                                                    <div>
                                                        No comments
                                                    </div>
                                                )
                                            }
                                        </div>
                                    </div>
                                    <Form.Label>Text</Form.Label>
                                    <ReactQuill theme="snow"
                                                value={this.state.rteValue ?? ""}
                                                onChange={(value) => this.contentChanged(value)}/>
                                </div>
                            </Form>
                        </Modal.Body>

                        <Modal.Footer className="Modal-Footer">
                            <Button className="btn Primary-Button" onClick={() => this.saveCard()}>Save</Button>
                            <Button className="btn Primary-Button"
                                    onClick={() => this.props.closeModal()}>Cancel</Button>
                        </Modal.Footer>
                    </Modal>
                )}
            </>
        );
    }
}