import React from "react";
import ICardView from "../../models/card/view/ICardView";
import {Card} from "react-bootstrap";
import "./TaskCard.css"
import TagsBadge from "./TagsBadge";
import TypeBadge from "./TypeBadge";

interface IProps {
    card: ICardView;
    onClick: Function;
}

interface IState {

}

export default class TaskCard extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);
    }

    toDate(timestamp: string): string{
        return new Date(Date.parse(timestamp)).toDateString();
    }

    render() {
        return (
            <Card className="Card" onClick={() => this.props.onClick()}>

                <Card.Title>
                    <Card.Text className="Card-Title">
                        {this.props.card.header}
                    </Card.Text>
                </Card.Title>

                <Card.Body>

                    <Card.Text>
                        {this.props.card.description}
                    </Card.Text>

                </Card.Body>

                <Card.Footer>


                    <Card.Text>
                        {this.toDate(this.props.card.createdTimestamp)}
                    </Card.Text>

                    <TypeBadge type={this.props.card.cardType}/>
                    <TagsBadge cardTags={this.props.card.cardTags}/>
                </Card.Footer>

            </Card>
        )
    }
}