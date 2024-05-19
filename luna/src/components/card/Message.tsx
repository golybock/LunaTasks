import React from "react";
import ICommentView from "../../models/card/view/ICommentView";
import "./Message.css";


interface IState{

}

interface IProps{
    comment: ICommentView,
    userId: string
}

export default class Message extends React.Component<IProps, any>{

    constructor(props: IProps) {
        super(props);
    }

    render() {
        return (
            <div>
                {this.props.comment.user?.id == this.props.userId && (
                    <div className="msg rcvd">{this.props.comment.comment}</div>
                )}
                {this.props.comment.user?.id != this.props.userId && (
                    <div className="msg sent">{this.props.comment.comment}</div>
                )}
            </div>
        );
    }
}