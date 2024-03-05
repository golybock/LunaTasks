import React from "react";
import "./Home.css"

interface IProps {

}

interface IState {

}

export default class Home extends React.Component<IProps, IState> {

    headerUrl ="https://localhost:7043/woodcuts_14.jpg";

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
                                <p>aboba 1</p>
                                <p>aboba 2</p>
                            </div>
                            <div className="Link-Block">
                                <h4>Life</h4>
                                <hr/>
                                {/*todo make this to link*/}
                                <p>aboba 1</p>
                                <p>aboba 2</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}