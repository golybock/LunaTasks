import React, {useEffect} from "react";
import IPageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import {Button, ButtonGroup, Table} from "react-bootstrap";
import "./Page.css"
import EditCardModal from "../card/EditCardModal";
import {CardDisplayMode} from "../../models/tools/CardDisplayMode";
import TaskCard from "../card/TaskCard";
import ICardView from "../../models/card/view/cardView";
import CardProvider from "../../provider/card/cardProvider";
import {useNavigate, useParams} from "react-router";

interface IProps {
    pageId: string
}

interface IState {
    id: string
    page: IPageView | null,
    cards: ICardView[],
    showModal: boolean,
    selectedCardId: string | null,
    displayMode: CardDisplayMode
}

function Page() {
    const {pageId} = useParams();

    return (
        <PageComponent pageId={pageId ?? ""}/>
    )
}

class PageComponent extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            id: this.props.pageId,
            page: null,
            cards: [],
            showModal: false,
            selectedCardId: null,
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

    async componentDidUpdate(prevProps: Readonly<IProps>, prevState: Readonly<IState>, snapshot?: any) {
        if (this.props.pageId != prevProps.pageId) {

            console.log("prev id:" + prevProps.pageId)
            console.log("new id:" + this.props.pageId)

            this.setState({id: this.props.pageId})
            this.setState({cards: []})

            let page = await PageProvider.getPage(this.props.pageId);

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
        this.setState({selectedCardId: null});

        let page = await PageProvider.getPage(this.props.pageId);

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
                                                                        this.setState({selectedCardId: card.id});
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
                                                                  this.setState({selectedCardId: card.id});
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

                    <EditCardModal pageId={this.props.pageId}
                                   cardId={this.state.selectedCardId}
                                   closeModal={async () => await this.closeModal()}/>

                )}

            </div>
        );
    }
}

export default Page