import React from "react";
import IPageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import {Guid} from "guid-typescript";
import "./Page.css"

interface IProps {
    // pageId : string;
}

interface IState {
    id: string | null
    page: IPageView | null
}

class Page extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            id: new URLSearchParams(window.location.search).get("id"),
            page: null
        }
    }

    async componentDidMount() {
        if(this.state.id != null){
            let page = await PageProvider.getPage(Guid.parse(this.state.id));

            if(page != null){
                this.setState({page: page});
                return;
            }

            // redirect to error
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
                        <div>
                            <h2>{this.state.page?.name}</h2>
                            <label>{this.state.page?.description}</label>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Page