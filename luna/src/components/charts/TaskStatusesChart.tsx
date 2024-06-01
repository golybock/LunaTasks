﻿import React from "react";
import {PieChart, PieValueType} from "@mui/x-charts";
import "./TaskStatesesChart.css";
import Form from "react-bootstrap/Form";
import {toDictionary} from "../../models/tools/ModelsConverter";
import PieValue from "../../models/charts/PieValue";
import ICardView from "../../models/card/view/ICardView";

interface IProps {
    cards: ICardView[]
}

interface IState {
    series: PieValueType[],
}

export default class TaskStatusesChart extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            series: []
        }
    }

    async componentDidMount() {
        const dict = toDictionary(this.props.cards);

        let val : number = 0;

        const array: PieValue[] = [];

        dict.forEach(item => {
            val++;
            array.push({id: val, statusName: JSON.parse(item.status)?.name.toString() ?? "Non status", count: item.card.length, color: JSON.parse(item.status)?.color})
        })

        const series = array.map(item => {
            return {value: item.count, label: item.statusName, id: item.id, color: item.color}
        })

        this.setState({series: series})
    }

    componentDidUpdate(prevProps: Readonly<IProps>, prevState: Readonly<IState>, snapshot?: any) {
        const dict = toDictionary(this.props.cards);

        let val : number = 0;

        const array: PieValue[] = [];

        dict.forEach(item => {
            val++;
            array.push({id: val, statusName: JSON.parse(item.status)?.name.toString() ?? "Non status", count: item.card.length, color: JSON.parse(item.status)?.color})
        })

        const series = array.map(item => {
            return {value: item.count, label: item.statusName, id: item.id, color: item.color}
        })

        if(prevProps != this.props){
            this.setState({series: series})
        }
    }

    render() {
        return (
            <div>
                <div className="Chart-Header">
                    <h2>{this.state.series.length} total tasks</h2>
                    <Form.Select className="Date-Picker"
                                 data-bs-theme="dark">
                        <option value="1">1 Month</option>
                        <option value="2">3 Months</option>
                        <option value="3">1 Year</option>
                    </Form.Select>
                </div>
                {this.state.series && (
                    <PieChart
                        series={[
                            {
                                data: this.state.series,
                                innerRadius: 80,
                                paddingAngle: 3,
                                cornerRadius: 5,
                            },
                        ]}
                        width={600}
                        height={250}/>
                )}
            </div>
        );
    }
}