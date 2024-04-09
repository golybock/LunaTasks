import React from "react";
import {pieArcLabelClasses, PieChart, PieValueType} from "@mui/x-charts";
import "./TaskStatesesChart.css";

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
                        sx={{
                            [`& .${pieArcLabelClasses.root}`]: {
                                fill: 'white',
                                fontWeight: 'bold',
                            },
                        }}
                        width={600}
                        height={250}/>
                )}
            </div>
        );
    }
}