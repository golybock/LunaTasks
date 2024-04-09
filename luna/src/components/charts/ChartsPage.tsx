import React from "react";
import TaskStatusesChart from "./TaskStatusesChart";
import GantChart from "./GantChart";
import ICardView from "../../models/card/view/cardView";
import CardProvider from "../../provider/card/cardProvider";
import PieValue from "../../models/charts/PieValue";
import {toDictionary} from "../../models/tools/ModelsConverter";

interface IProps{
    pageId: string;
}

interface IState{
    cards: ICardView[],
    statuses: PieValue[]
}

export default class ChartsPage extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);

        this.state = {
            cards: [],
            statuses: []
        }
    }

    async componentDidMount() {
        const cards = await CardProvider.getCards(this.props.pageId);

        this.setState({cards: cards});

        const dict = toDictionary(cards);

        dict.forEach(item => {
            this.state.statuses.push({id: 0, statusName: JSON.parse(item.status).name?.toString() ?? "", count: item.card.length})
        })
    }

    render() {
        return (
            <div>
                {this.state.statuses && (
                    <TaskStatusesChart values={this.state.statuses}/>
                )}
                {this.state.cards && (
                    <GantChart cards={this.state.cards}/>
                )}
            </div>
        );
    }
}