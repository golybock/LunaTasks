import React from "react";
import IPageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import {Button, ButtonGroup, Table} from "react-bootstrap";
import "./Page.css"
import EditCardModal from "../card/EditCardModal";
import {CardDisplayMode} from "../../models/tools/CardDisplayMode";
import TaskCard from "../card/TaskCard";
import ICardView from "../../models/card/view/cardView";
import CardProvider from "../../provider/card/cardProvider";

interface IProps {

}

interface IState {
    id: string
    page: IPageView | null,
    cards: ICardView[],
    showModal: boolean,
    selectedPageId: string | null,
    displayMode: CardDisplayMode
}

class Page extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            id: new URLSearchParams(window.location.search).get("id") ?? "",
            page: null,
            cards: [],
            showModal: false,
            selectedPageId: null,
            displayMode: CardDisplayMode.Table
        }
    }

    async componentDidMount() {
        if (this.state.id != null) {
            let page = await PageProvider.getPage(this.state.id);

            if (page != null) {
                this.setState({page: page});

                const cards = await CardProvider.getCards(page.id);

                this.setState({cards: cards});

                return;
            }

            // redirect to error
        }
    }

    showModal() {
        this.setState({showModal: true})
    }

    async closeModal() {
        this.setState({showModal: false});
        this.setState({selectedPageId: null});

        let page = await PageProvider.getPage(this.state.id);

        if (page != null) {
            this.setState({page: page});
            return;
        }
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
                                <Button className="btn btn-outline-dark"
                                        onClick={() => {
                                            this.setState({displayMode: CardDisplayMode.Table});
                                        }}>Table</Button>
                                <Button className="btn btn-outline-dark"
                                        onClick={() => {
                                            this.setState({displayMode: CardDisplayMode.Card});
                                        }}>Cards</Button>
                            </ButtonGroup>

                            <Button className="btn btn-outline-dark Button" onClick={() => {
                                this.setState({showModal: true})
                            }}>New</Button>

                        </div>
                        <div className="Page-Content-Data">
                            {this.state.cards &&
                                (
                                    <>
                                        {this.state.displayMode == CardDisplayMode.Table && (
                                            <Table className="table-dark table-bordered">
                                                <thead>
                                                <tr>
                                                    <th>Header</th>
                                                    <th>Type</th>
                                                    <th>Created</th>
                                                    <th>Edit</th>
                                                </tr>
                                                </thead>
                                                <tbody>
                                                {this.state.cards.map(((card) => (
                                                    <tr key={card.id.toString()}>
                                                        <td>{card.header}</td>
                                                        <td>{card.cardType.name}</td>
                                                        <td>{new Date(Date.parse(card.createdTimestamp)).toDateString()}</td>
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
                                        )}
                                        {this.state.displayMode == CardDisplayMode.Card && (
                                            <div className="Cards">
                                                {this.state.cards.map(((card) => (
                                                    <TaskCard card={card}
                                                              key={card.id}
                                                              onClick={() => {
                                                                  this.setState({selectedPageId: card.id});
                                                                  this.showModal();
                                                              }}/>
                                                )))}
                                            </div>
                                        )}
                                    </>
                                )
                            }
                        </div>
                    </div>
                </div>

                {this.state.showModal && (

                    <EditCardModal pageId={this.state.id}
                                   cardId={this.state.selectedPageId}
                                   closeModal={async () => await this.closeModal()}/>

                )}

            </div>
        );
    }
}

export default Page