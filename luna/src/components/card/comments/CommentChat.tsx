import React from "react";
import ICommentView from "../../../models/card/view/ICommentView";
import Message from "../Message";
import "./CommentChat.css";
import {AuthWrapper} from "../../../auth/AuthWrapper";


interface IState{
    userId: string,
    comments: ICommentView[],
}

interface IProps {
    comments: ICommentView[]
}

export default class CommentChat extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            userId: AuthWrapper.userId() ?? "",
            comments: this.props.comments,
        }
    }

    componentDidUpdate(prevProps: Readonly<IProps>, prevState: Readonly<IState>, snapshot?: any) {
        if(prevProps != this.props){
            this.setState({comments: this.props.comments})
            console.log("comments updated")
        }
    }

    render() {
        return (
            <div className="chat">
                {this.state.comments.map((item) => {
                    return <Message comment={item} key={item.id}/>
                })}
            </div>
        );
    }
}