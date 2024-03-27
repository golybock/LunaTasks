import React from "react";
import Spinner from 'react-bootstrap/Spinner';
import "./Loading.css"

export default class Loading extends React.Component<any, any>{
    render() {
        return (
            <div className="Loading-Page">
                <Spinner className="Loader" animation="border" role="status">
                    <span className="visually-hidden">Loading...</span>
                </Spinner>
            </div>
        );
    }
}