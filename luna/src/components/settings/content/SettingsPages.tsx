import React from "react";
import "./SettingsStatuses.css"
import NotificationManager from "../../../tools/NotificationManager";
import IPageView from "../../../models/page/IPageView";
import PageProvider from "../../../provider/page/pageProvider";
import {WorkspaceManager} from "../../../tools/WorkspaceManager";
import PageModal from "../modals/PageModal";

interface IProps {

}

interface IState {
    pages: IPageView[],
    showPageModal: boolean
}

export default class SettingsPages extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            pages: [],
            showPageModal: false
        }
    }

    async componentDidMount() {
        let pages = await PageProvider.getPages(WorkspaceManager.getWorkspace()!);
        this.setState({pages: pages})
    }

    showPageModal() {
        this.setState({showPageModal: true})
    }
    
    async closePageModal() {
        this.setState({showPageModal: false})

        let pages = await PageProvider.getPages(WorkspaceManager.getWorkspace()!);
        this.setState({pages: pages})
    }

    async deletePage(id: string) {
        const res = await PageProvider.deletePage(id);

        if (res) {
            let pages = await PageProvider.getPages(WorkspaceManager.getWorkspace()!);
            this.setState({pages: pages})

            NotificationManager.makeSuccess("Page deleted!")
        } else {
            NotificationManager.makeError("Error")
        }
    }

    render() {
        return (
            <div className="Settings-Statuses">
                <div className="Item-Block" id="sttuses">
                    <div className="Item-Header">
                        <h4>Pages</h4>

                        {this.state.pages.length < 5 && (
                            <button className="btn Outline-Button" onClick={() => this.showPageModal()}>+</button>
                        )}
                    </div>
                    <hr/>
                    {this.state.pages ?
                        (
                            this.state.pages.map((item) => {
                                return (<div className="Item" key={item.id}>
                                    <label>{item.name}</label>
                                    <div className="row">
                                        <button className="btn Outline-Button"
                                                onClick={() => this.deletePage(item.id)}>-
                                        </button>
                                    </div>
                                </div>)
                            })
                        )
                        :
                        (<div>No elements</div>)}
                </div>
                {this.state.showPageModal && (
                    <PageModal pageId={null} closeModal={() => this.closePageModal()}/>
                )}
            </div>
        );
    }
}
