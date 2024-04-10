import React from "react";
import ITagView from "../../models/card/view/ITagView";
import "./TagsBadge.css";

interface IProps {
    cardTags: ITagView[]
}

interface IState {
}

export default class TagsBadge extends React.Component<IProps, IState>{


    render() {
        return (
            <div className="Badge-Container">
                {this.props.cardTags.map((tag) => {
                    return(
                        <div className="Tag" style={{background: tag.color}}>
                            <span>{tag.name}</span>
                        </div>
                    )
                })}
            </div>
        );
    }
}