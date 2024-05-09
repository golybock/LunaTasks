import React from "react";
import ICardView from "../../../models/card/view/ICardView";
import TypeBadge from "../../card/TypeBadge";
import TagsBadge from "../../card/TagsBadge";
import "./CardRow.css";

interface IProps {
    card: ICardView;
    onClick: Function;
}

interface IState {

}

export default class CardRow extends React.Component<any, any>{
    constructor(props: IProps) {
        super(props);
    }

    toDate(timestamp: string): string{
        return new Date(Date.parse(timestamp)).toISOString().split('T')[0];
    }

    render() {
        return (
            <div className="Card-Row" onClick={() => this.props.onClick()}>

                <div className="Card-Row-Header">
                    <div className="Row-Color-Block" style={{border: `6px solid ${ this.props.card?.status?.color ?? "white" }`}}></div>
                    <h5>{this.props.card.header}</h5>
                </div>

                <div className="Card-Row-Body">
                    <div className="Date">
                        <img src={"/icons/date.svg"}/>
                        <label>{this.toDate(this.props.card.createdTimestamp)}</label>
                    </div>

                    <div className="Card-Row-Tags">
                        <TypeBadge type={this.props.card.cardType}/>
                        {/*<TagsBadge cardTags={this.props.card.cardTags}/>*/}
                    </div>
                </div>

                <button className="Button Outline-Button">Редактировать</button>

            </div>
        )
    }
}