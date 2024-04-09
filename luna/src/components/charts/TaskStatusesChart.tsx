import React from "react";
import {PieChart, PieValueType} from "@mui/x-charts";
import PieValue from "../../models/charts/PieValue";

interface IProps {
    series: PieValueType[]
}

interface IState {
}

export default class TaskStatusesChart extends React.Component<IProps, IState> {
    componentDidMount() {

    }

    render() {
        return (
            <div>
                {this.props.series && (
                    <PieChart
                        series={[
                            {
                                data: this.props.series,
                            },
                        ]}
                        width={400}
                        height={250}/>
                )}
            </div>
        );
    }
}