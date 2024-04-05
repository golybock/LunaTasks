import React from "react";
import ICardView from "../../models/card/view/cardView";
import {Card} from "react-bootstrap";
import "./TaskCard.css"

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

    render() {
        return (
            <Card className="Card" onClick={() => this.props.onClick()}>

                <Card.Title>
                    <Card.Text>
                        {this.props.card.header}
                    </Card.Text>
                </Card.Title>

                <Card.Body>

                    <Card.Text>
                        {this.props.card.description}
                    </Card.Text>

                    <Card.Text>
                        {this.props.card.createdTimestamp}
                    </Card.Text>

                </Card.Body>

            </Card>
        )
    }
}