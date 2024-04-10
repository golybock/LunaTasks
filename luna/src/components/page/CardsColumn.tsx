import React from "react";
import ICardView from "../../models/card/view/ICardView";
import ColumnHeader from "./ColumnHeader";
import IStatusView from "../../models/card/view/IStatusView";
import TaskCard from "../card/TaskCard";
import "./CardsColumn.css";

interface IProps{
    cards: ICardView[],
    status: IStatusView,
    showModal: Function,
    setSelected: Function
}

interface IState{
}

export default class CardsColumn extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);
    }

    render() {
        return (
            <div className="Cards-Column">
                <ColumnHeader status={this.props.status} count={this.props.cards.length}/>

                <div className="Cards">
                    {this.props.cards.map(((card) => (
                        <TaskCard card={card}
                                  key={card.id}
                                  onClick={() => {
                                      this.props.setSelected(card.id);
                                      this.props.showModal();
                                  }}/>
                    )))}
                </div>

            </div>
        );
    }
}