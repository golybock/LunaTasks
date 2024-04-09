import React from "react";
import {Gantt, Task} from "gantt-task-react";
import ICardView from "../../models/card/view/cardView";

interface IProps{
    cards: ICardView[]
}

interface IState{

}

let tasks: Task[] = [
    {
        start: new Date(2020, 1, 1),
        end: new Date(2020, 1, 2),
        name: 'Idea',
        id: 'Task 0',
        type:'task',
        progress: 45,
        isDisabled: true,
        styles: { progressColor: '#ffbb54', progressSelectedColor: '#ff9e0d' },
    }
];

export default class GantChart extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);

        this.state = {

        }
    }

    componentDidMount() {
        // create tasks to view
    }

    render() {
        return (
            <div>
                <Gantt tasks={tasks}/>
            </div>
        );
    }
}