import React from "react";
import IPageView from "../../models/page/IPageView";
import PageProvider from "../../provider/page/pageProvider";
import {Button, ButtonGroup, Table} from "react-bootstrap";
import "./Page.css"
import EditCardModal from "../card/modals/EditCardModal";
import {CardDisplayMode} from "../../models/tools/CardDisplayMode";
import ICardView from "../../models/card/view/ICardView";
import CardProvider from "../../provider/card/cardProvider";
import {useParams} from "react-router";
import DarkAsyncSelect from "../tools/DarkAsyncSelect";
import {MultiValue, SingleValue} from "react-select";
import IOption from "../../models/tools/IOption";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import {WorkspaceManager} from "../../tools/WorkspaceManager";
import Loading from "../notifications/Loading";
import CardsColumn from "./CardsColumn";
import {toDictionary} from "../../models/tools/ModelsConverter";
import CardsTable from "./table/CardsTable";

interface IProps {
    pageId: string
}

interface IState {
    id: string
    isLoading: boolean,
    page: IPageView | null,
    cards: ICardView[],
    showModal: boolean,
    selectedCardId: string | null,
    displayMode: CardDisplayMode,
    filterUserId: IOption | null
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
            isLoading: true,
            page: null,
            cards: [],
            showModal: false,
            selectedCardId: null,
            displayMode: CardDisplayMode.Card,
            filterUserId: null
        }
    }

    async componentDidMount() {
        if (this.state.id != null) {

            this.setState({isLoading: true})

            let page = await PageProvider.getPage(this.state.id);

            if (page != null) {
                this.setState({page: page});

                const cards = await CardProvider.getCards(page.id);

                console.log(toDictionary(this.state.cards))

                this.setState({cards: cards});

                this.setState({isLoading: false})

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

            this.setState({isLoading: true})

            let page = await PageProvider.getPage(this.props.pageId);

            if (page != null) {
                this.setState({page: page});

                const cards = await CardProvider.getCards(page.id);

                this.setState({cards: cards});

                this.setState({isLoading: false})

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
        this.setState({isLoading: true})

        let page = await PageProvider.getPage(this.props.pageId);

        if (page != null) {
            this.setState({page: page});

            const cards = await CardProvider.getCards(page.id);

            this.setState({cards: cards});

            this.setState({isLoading: false})

            return;
        }
    }

    async getUsers() {
        return await WorkspaceProvider.getWorkspaceUsersOptions(WorkspaceManager.getWorkspace()!);
    }

    async userSelected(e: SingleValue<IOption>) {
        this.setState({filterUserId: e as IOption})

        if (e) {
            const cards = await CardProvider.getCardsByUserIds(this.props.pageId, [e.value]);

            this.setState({cards: cards});
        } else {
            const cards = await CardProvider.getCards(this.props.pageId);

            this.setState({cards: cards});
        }
    }


    render() {
        return (
            <div>
                {this.state.isLoading && (
                    <Loading/>
                )}
                {!this.state.isLoading && (
                    <div>
                        {/*{this.state.page?.headerImage && (*/}
                        {/*    <div className="Header-Image-Container">*/}
                        {/*        <img src={this.state.page?.headerImage} alt=""/>*/}
                        {/*    </div>*/}
                        {/*)}*/}
                        <div className="Page-Content">
                            <div>
                                <div className="Page-Content-Header">
                                    <h1>{this.state.page?.name}</h1>
                                    <label>{this.state.page?.description}</label>
                                </div>
                                <div className="Page-Content-Toolbar">

                                    <div className="Page-Content-Toolbar-Item">
                                        <ButtonGroup>
                                            <Button className="Outline-Button"
                                                    onClick={() => {
                                                        this.setState({displayMode: CardDisplayMode.Table});
                                                    }}>
                                                <div>
                                                    <img className="Icon" src={"/icons/table.svg"}/>
                                                    <label>Table</label>
                                                </div>
                                            </Button>
                                            <Button className="Outline-Button"
                                                    onClick={() => {
                                                        this.setState({displayMode: CardDisplayMode.Card});
                                                    }}>
                                                <img className="Icon" src={"/icons/cards.svg"}/>
                                                <label>Cards</label>
                                            </Button>
                                        </ButtonGroup>

                                        {/*<div className="Button Page-Content-Toolbar-Select">*/}
                                        {/*    <DarkAsyncSelect isMulti={false}*/}
                                        {/*                     cacheOptions*/}
                                        {/*                     defaultOptions*/}
                                        {/*                     value={this.state.filterUserId}*/}
                                        {/*                     loadOptions={this.getUsers}*/}
                                        {/*                     onChange={(e: SingleValue<IOption>) => this.userSelected(e)}/>*/}
                                        {/*</div>*/}
                                    </div>

                                    <div className="Page-Content-Toolbar-Item">
                                        <Button className="Primary-Button Button" onClick={() => {
                                            this.setState({showModal: true})
                                        }}>

                                            <label>Create new</label>
                                        </Button>

                                        <Button className="Secondary-Button Button" onClick={async () => {
                                            await PageProvider.getPageReport(this.props.pageId);
                                        }}>
                                            <img src={"/icons/xlsx.svg"}/>
                                        </Button>
                                    </div>

                                </div>

                                <hr/>

                                <div className="Page-Content-Data">
                                    {this.state.cards &&
                                        (
                                            <>
                                                {this.state.displayMode == CardDisplayMode.Table && (
                                                    <CardsTable cards={this.state.cards}
                                                                setSelected={(e: string) => {
                                                                    this.setState({selectedCardId: e})
                                                                }}
                                                                showModal={() => this.showModal()}/>
                                                )}
                                                {this.state.displayMode == CardDisplayMode.Card && (
                                                    <div className="Cards">
                                                        {toDictionary(this.state.cards).map(((item) => (
                                                            <CardsColumn cards={item.card}
                                                                         key={item.status}
                                                                         setSelected={(e: string) => {
                                                                             this.setState({selectedCardId: e})
                                                                         }}
                                                                         status={JSON.parse(item.status) ?? {
                                                                             name: "Non status",
                                                                             color: "#FFFFFF"
                                                                         }}
                                                                         showModal={() => this.showModal()}/>
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
                )}
            </div>
        );
    }
}

export default Page