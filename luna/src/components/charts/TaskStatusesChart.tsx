import React from "react";
import {PieChart, PieValueType} from "@mui/x-charts";
import PieValue from "../../models/charts/PieValue";

interface IProps {
    values: PieValue[]
}

interface IState {
    series: PieValueType[]
}

export default class TaskStatusesChart extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            series: this.props.values.map(item => {
                return {value: item.count, label: item.statusName, id: item.id}
            })
        }
    }

    render() {
        return (
            <div>
                {this.state.series && (
                    <PieChart
                        series={[
                            {
                                data: this.state.series,
                            },
                        ]}
                        width={400}
                        height={250}/>
                )}
            </div>
        );
    }
}