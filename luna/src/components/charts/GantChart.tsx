import React from "react";
import {Gantt, Task, ViewMode} from 'gantt-task-react';
import "gantt-task-react/dist/index.css";
import ICardView from "../../models/card/view/ICardView";
import "./GantChart.css";

interface IProps{
    cards: ICardView[]
}

interface IState{
    tasks: Task[]
}

export default class GantChart extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);

        this.state = {
            tasks: []
        }
    }

    componentDidMount() {
        const arr: Task[] = []

        this.props.cards.forEach(item => {
            arr.push({
                start: new Date(2024, 1, 1),
                end: new Date(2024, 1, 2),
                name: item.header,
                id: item.id,
                type:'task',
                progress: 100,
                isDisabled: true,
                styles: { progressColor: '#ffbb54', progressSelectedColor: '#ff9e0d' },
            })
        })

        this.setState({tasks: arr})
    }

    render() {
        return (
            <div className="Diagram">
                {this.state.tasks && (
                    <Gantt tasks={this.state.tasks}
                           columnWidth={40}
                           listCellWidth=""
                           locale={"ru"}/>
                )}
            </div>
        );
    }
}