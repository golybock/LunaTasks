import React from "react";
import IPageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import {Button, ButtonGroup, Table} from "react-bootstrap";
import EditPageModal from "./EditPageModal";
import "./Page.css"

interface IProps {

}

interface IState {
    id: string
    page: IPageView | null,
    showModal: boolean,
    selectedPageId : string | null
}

class Page extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            id: new URLSearchParams(window.location.search).get("id") ?? "",
            page: null,
            showModal: false,
            selectedPageId: null
        }
    }

    async componentDidMount() {
        if (this.state.id != null) {
            let page = await PageProvider.getPage(this.state.id);

            if (page != null) {
                this.setState({page: page});
                return;
            }

            // redirect to error
        }
    }

    async componentDidUpdate() {
        if (this.state.id != null) {
            let page = await PageProvider.getPage(this.state.id);

            if (page != null) {
                this.setState({page: page});
                return;
            }

            // redirect to error
        }
    }

    showModal() {
        this.setState({showModal: true})
    }

    closeModal() {
        this.setState({showModal: false})
    }

    render() {
        return (
            <div>
                {this.state.page?.headerImage && (
                    <div className="Header-Image-Container">
                        <img src={this.state.page?.headerImage} alt=""/>
                    </div>
                )}
                <div className="Page-Content">
                    <div>
                        <div className="Page-Content-Header">
                            <h2>{this.state.page?.name}</h2>
                            <label>{this.state.page?.description}</label>
                        </div>
                        <div className="Page-Content-Toolbar">

                            <ButtonGroup>
                                <Button className="btn btn-outline-dark">Table</Button>
                                <Button className="btn btn-outline-dark">Cards</Button>
                            </ButtonGroup>

                            <Button className="btn btn-outline-dark Button" onClick={() => {
                                this.setState({showModal: true})
                            }}>New</Button>

                        </div>
                        <div className="Page-Content-Data">
                            {this.state.page?.cards &&
                                (
                                    <Table className="table-dark">
                                        <thead>
                                        <tr>
                                            <th>Header</th>
                                            <th>Type</th>
                                            <th>Created</th>
                                            <th>Edit</th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        {this.state.page?.cards.map(((card) => (
                                            <tr key={card.id.toString()}>
                                                <td>{card.header}</td>
                                                <td>{card.cardType.name}</td>
                                                <td>{new Date(Date.parse(card.createdTimestamp)).toLocaleDateString()}</td>
                                                <td>
                                                    <Button className="btn btn-outline-dark Table-Button"
                                                            onClick={() => {
                                                                this.setState({selectedPageId: card.id});
                                                                this.showModal();
                                                            }}>
                                                        Edit
                                                    </Button>
                                                </td>
                                            </tr>
                                        )))}
                                        </tbody>
                                    </Table>
                                )
                            }
                        </div>
                    </div>
                </div>

                {this.state.showModal && (
                    <EditPageModal pageId={this.state.id} cardId={this.state.selectedPageId} closeModal={() => this.closeModal()}/>
                )}

            </div>
        );
    }
}

export default Page