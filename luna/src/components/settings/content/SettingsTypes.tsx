import React from "react";
import "./SettingsTypes.css";
import TypeProvider from "../../../provider/card/typeProvider";
import TypeView from "../../../models/card/view/ITypeView";
import NotificationManager from "../../../tools/NotificationManager";
import {Button} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import TypeModal from "../modals/TypeModal";

interface IProps {

}

interface IState {
    types: TypeView[],
    showTypeModal: boolean,
}

export default class SettingsTypes extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            types: [],
            showTypeModal: false
        }
    }

    async componentDidMount() {
        const types = await TypeProvider.getTypes();
        this.setState({types: types});

    }

    showTypeModal() {
        this.setState({showTypeModal: true})
    }

    async closeTypeModal() {
        this.setState({showTypeModal: false})

        const types = await TypeProvider.getTypes();
        this.setState({types: types});
    }

    async deleteType(id: string) {
        const res = await TypeProvider.deleteType(id);

        if (res) {
            const types = await TypeProvider.getTypes();
            this.setState({types: types});
        } else {
            NotificationManager.makeError("Error")
        }
    }

    render() {
        return (
            <div className="Settings-Types">
                <div className="Item-Block" id="types">
                    <div className="Item-Header">
                        <h4>Типы</h4>
                        <Button className="btn Outline-Button" onClick={() => this.showTypeModal()}>+
                        </Button>
                    </div>
                    <hr/>
                    {this.state.types ?
                        (
                            this.state.types.map((item) => {
                                return (<div className="Item" key={item.id}>
                                    <label>{item.name}</label>
                                    <div className="row">
                                        <Form.Control disabled type="color" value={item.color}/>
                                        <button className="btn Outline-Button"
                                                onClick={() => this.deleteType(item.id)}>-
                                        </button>
                                    </div>
                                </div>)
                            })
                        )
                        :
                        (<div>Нет элементов</div>)
                    }
                </div>
                    {this.state.showTypeModal && (
                        <TypeModal closeModal={() => this.closeTypeModal()}/>
                    )}
            </div>
        );
    }
}
