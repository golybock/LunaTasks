import React from "react";
import ICommentView from "../../models/card/view/ICommentView";
import "./Message.css";
import IUserView from "../../models/user/IUserView";
import UserProvider from "../../provider/user/userProvider";


interface IState{
    user?: IUserView
}

interface IProps{
    comment: ICommentView,
}

export default class Message extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);

        this.state = {
            user: undefined
        }
    }

    async componentDidMount() {
        console.log(this.props.comment)
        const user = await UserProvider.getUser(this.props.comment.userId ?? this.props.comment.user?.id);
        console.log(user?.image)

        if(user != null){
            this.setState({user: user})
        }
    }

    render() {
        return (
            <div className="Message-Block">
                <div>
                    <img className="User-Image" src={this.state.user?.image}/>
                </div>
                <div className="Message">{this.props.comment.comment}</div>
            </div>
        );
    }
}