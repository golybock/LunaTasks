import React from "react";
import {PieChart} from "@mui/x-charts";

interface IProps {

}

interface IState {

}

export default class TaskStatusesChart extends React.Component<IProps, IState> {
    render() {
        return (
            <div>
                <PieChart
                    series={[
                        {
                            data: [
                                {id: 0, value: 10, label: 'series A'},
                                {id: 1, value: 15, label: 'series B'},
                                {id: 2, value: 20, label: 'series C'},
                            ],
                        },
                    ]}
                    width={400}
                    height={250}/>
            </div>
        );
    }
}