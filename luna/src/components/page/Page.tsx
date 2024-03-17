import React from "react";
import {RouterProps} from "react-router";
import IPageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import {Guid} from "guid-typescript";

interface IProps {
    // pageId : string;
}

interface IState {
    id: string | null
    page: IPageView | null
}

class Page extends React.Component<IProps, IState> {

    headerUrl = "http://localhost:7005/woodcuts_14.jpg";

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
                <div className="Header-Image-Container">
                    <img src={this.headerUrl} alt=""/>
                </div>
                <div className="Home-Content">
                    <div>
                        <div>
                            <h2>Page name: {this.state.page?.name}</h2>
                            <h2>Page description: {this.state.page?.description}</h2>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Page