import React from "react";
import ICommentView from "../../../models/card/view/ICommentView";
import Message from "../Message";
import "./CommentChat.css";
import {AuthWrapper} from "../../../auth/AuthWrapper";


interface IState{
    userId: string
}

interface IProps {
    comments: ICommentView[]
}

export default class CommentChat extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            userId: AuthWrapper.user() ?? ""
        }
    }

    render() {
        return (
            <div className="chat">
                {this.props.comments.map((item) => {
                    return <Message comment={item} userId={this.state.userId}/>
                })}
            </div>
        );
    }
}