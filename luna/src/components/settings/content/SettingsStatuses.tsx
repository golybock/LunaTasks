import React from "react";
import "./SettingsStatuses.css"
import StatusProvider from "../../../provider/card/statusProvider";
import NotificationManager from "../../../tools/NotificationManager";
import IStatusView from "../../../models/card/view/IStatusView";
import Form from "react-bootstrap/Form";
import StatusModal from "../modals/StatusModal";

interface IProps {

}

interface IState {
    statuses: IStatusView[],
    showStatusModal: boolean
}

export default class SettingsStatuses extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            statuses: [],
            showStatusModal: false
        }
    }

    async componentDidMount() {
        const statuses = await StatusProvider.getStatuses();
        this.setState({statuses: statuses})
    }

    showStatusModal() {
        this.setState({showStatusModal: true})
    }

    async closeStatusModal() {
        this.setState({showStatusModal: false})

        const statuses = await StatusProvider.getStatuses();
        this.setState({statuses: statuses})
    }

    async deleteStatus(id: string) {
        const res = await StatusProvider.deleteStatus(id);

        if (res) {
            const statuses = await StatusProvider.getStatuses();
            this.setState({statuses: statuses})
        } else {
            NotificationManager.makeError("Error")
        }
    }

    render() {
        return (
            <div className="Settings-Statuses">
                <div className="Item-Block" id="sttuses">
                    <div className="Item-Header">
                        <h4>Статусы</h4>
                        <button className="btn Outline-Button" onClick={() => this.showStatusModal()}>+
                        </button>
                    </div>
                    <hr/>
                    {this.state.statuses ?
                        (
                            this.state.statuses.map((item) => {
                                return (<div className="Item" key={item.id}>
                                    <label>{item.name}</label>
                                    <div className="row">
                                        <Form.Control disabled type="color" value={item.color}/>
                                        <button className="btn Outline-Button"
                                                onClick={() => this.deleteStatus(item.id)}>-
                                        </button>
                                    </div>
                                </div>)
                            })
                        )
                        :
                        (<div>Нет элементов</div>)}
                </div>
                    {this.state.showStatusModal && (
                        <StatusModal closeModal={() => this.closeStatusModal()}/>
                    )}
            </div>
        );
    }
}
