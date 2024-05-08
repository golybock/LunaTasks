import React from "react";
import ICardView from "../../../models/card/view/ICardView";
import CardRow from "./CardRow";
import "./CardsTable.css";

interface IProps {
    cards: ICardView[],
    setSelected: Function,
    showModal: Function
}

interface IState {
}

export default class CardsTable extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
    }

    render() {
        return (
            <div className="Cards-Column">

                <div className="Cards-Table">
                    {this.props.cards.map(((card) => (
                        <CardRow card={card}
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