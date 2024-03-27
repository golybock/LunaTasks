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

                {/*<Card.Img variant="top"*/}
                {/*          style={{padding: "15px"}}*/}
                {/*          src={this.props.card.im}/>*/}

                <Card.Title>
                    <Card.Text>
                        {this.props.card.header}
                    </Card.Text>
                </Card.Title>

                <Card.Body>

                    <Card.Text>
                        Description: {this.props.card.description}
                    </Card.Text>

                    <Card.Text>
                        Created: {this.props.card.createdTimestamp}
                    </Card.Text>

                </Card.Body>

            </Card>
        )
    }
}