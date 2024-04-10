import IStatusView from "../../models/card/view/IStatusView";
import React from "react";
import "./ColumnHeader.css"

interface IProps{
    status: IStatusView,
    count: number
}

interface IState{

}

export default class ColumnHeader extends React.Component<IProps, IState>{

    render() {
        return (
            <div className="Status-Container">
                <span style={{background: this.props.status.color}} className="Status">{this.props.status.name}</span>
                <span>{this.props.count}</span>
            </div>
        );
    }
}