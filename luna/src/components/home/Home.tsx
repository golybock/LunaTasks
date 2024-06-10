import React from "react";
import "./Home.css"

interface IProps {

}

interface IState {

}

export default class Home extends React.Component<IProps, IState> {

    headerUrl ="http://localhost:7005/woodcuts_14.jpg";

    render() {
        return (
            <div>
                <div className="Header-Image-Container">
                    <img src={this.headerUrl} alt=""/>
                </div>
                <div className="Home-Content">
                    <div>
                        <div className="Header">
                            <div>
                                <h1>Personal home</h1>
                                <label>Organize everything this</label>
                            </div>
                        </div>
                        <div className="Links">
                            <div className="Link-Block">
                                <h4>Daily</h4>
                                <hr/>
                                {/*todo make this to link*/}
                                <p>Your active tasks</p>
                                <p>Your tasks</p>
                            </div>
                            <div className="Link-Block">
                                <h4>Management</h4>
                                <hr/>
                                {/*todo make this to link*/}
                                <p>View statistic</p>
                                <p>Settings</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}