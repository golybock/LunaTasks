import React from "react";
import ICommentView from "../../../models/card/view/ICommentView";
import Message from "../Message";
import "./CommentChat.css";
import {AuthWrapper} from "../../../auth/AuthWrapper";


interface IState{
    userId: string,
    comments: ICommentView[]
}

interface IProps {
    comments: ICommentView[]
}

export default class CommentChat extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            userId: AuthWrapper.user() ?? "",
            comments: this.props.comments
        }
    }

    render() {
        return (
            <div className="chat">
                {this.state.comments.map((item) => {
                    return <Message comment={item} userId={this.state.userId} key={item.id}/>
                })}
            </div>
        );
    }
}