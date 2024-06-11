import React from "react";
import TaskStatusesChart from "./TaskStatusesChart";
import ICardView from "../../models/card/view/ICardView";
import CardProvider from "../../provider/card/cardProvider";
import PieValue from "../../models/charts/PieValue";
import {toDictionary} from "../../models/tools/ModelsConverter";
import {PieValueType} from "@mui/x-charts";
import "./ChartsPage.css"
import GantChart from "./GantChart";
import TaskUsersChart from "./TaskUsersChart";

interface IProps{
}

interface IState{
    cards: ICardView[],
    statuses: PieValue[],
    values: PieValueType[]
}

export default class ChartsPage extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);

        this.state = {
            cards: [],
            statuses: [],
            values: []
        }
    }

    async componentDidMount() {
        const cards = await CardProvider.getCardsByWorkspace();

        this.setState({cards: cards});

        const dict = toDictionary(cards);

        let val : number = 0;

        const array: PieValue[] = [];

        dict.forEach(item => {
            val++;
            array.push({id: val, statusName: JSON.parse(item.status)?.name.toString() ?? "Non status", count: item.card.length, color: JSON.parse(item.status)?.color})
        })

        this.setState({statuses: array});

        const series = array.map(item => {
            return {value: item.count, label: item.statusName, id: item.id, color: item.color}
        })

        this.setState({values: series})
    }

    headerUrl = "http://localhost:7005/woodcuts_14.jpg";

    render() {
        return (
            <div>

                <div className="Header-Image-Container">
                    <img src={this.headerUrl} alt=""/>
                </div>
                <div className="About-Content">
                    <div>
                        <div className="Header">
                            <div className="Chart-Header">
                                <h1>Statistic page</h1>
                            </div>
                        </div>
                        <div className="Items">
                            <div className="Graph-Item">
                                {this.state.values && (
                                    <TaskStatusesChart cards={this.state.cards}/>
                                )}
                            </div>
                            <div className="Graph-Item">
                                {this.state.values && (
                                    <TaskUsersChart cards={this.state.cards}/>
                                )}
                            </div>
                        </div>
                        {/*<div className="Items">*/}
                        {/*    <div className="Graph-Item-Full">*/}
                        {/*           <h2>Gantt </h2>*/}
                        {/*        {this.state.cards && (*/}
                        {/*            <div></div>*/}
                        {/*            // <GantChart cards={this.state.cards}/>*/}
                        {/*        )}*/}
                        {/*    </div>*/}
                        {/*</div>*/}
                    </div>
                </div>

            </div>
        );
    }
}