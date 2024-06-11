import React from "react";
import ICardView from "../../models/card/view/ICardView";
import "./TaskCard.css"
import TagsBadge from "./TagsBadge";
import TypeBadge from "./TypeBadge";

interface IProps {
    card: ICardView;
    onClick: Function;
}

interface IState {

}

export default class TaskCard extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);
    }

    toDate(timestamp: string): string{
        return new Date(Date.parse(timestamp)).toISOString().split('T')[0];
    }

    render() {
        return (
            <div className="Card" onClick={() => this.props.onClick()}>

                <div className="Card-Title">
                    <h5>{this.props.card.header}</h5>
                </div>

                <hr/>

                <div>
                    {this.props.card.description}
                </div>

                <div className="Date">
                    <img className="Icon" src={"/icons/date.svg"}/>
                    <label>{this.toDate(this.props.card.createdTimestamp)}</label>
                </div>

                <div className="Tags">
                    <TypeBadge type={this.props.card.cardType}/>
                    <TagsBadge cardTags={this.props.card.cardTags}/>
                </div>


            </div>
        )
    }
}