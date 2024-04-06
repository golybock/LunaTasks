import React from "react";
import TagView from "../../models/card/view/tagView";
import "./TagsBadge.css";

interface IProps {
    cardTags: TagView[]
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