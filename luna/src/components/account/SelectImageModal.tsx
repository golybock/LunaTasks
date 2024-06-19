import React from "react";
import {Modal} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import DataProvider from "../../provider/data/dataProvider";
import {WorkspaceManager} from "../../tools/WorkspaceManager";
import "./SelectImageModal.css";

interface IProps {
    closeModal: Function,
    changeImage: Function,
    imagePath: string,
}

interface IState {
    formFile: FileList | null,
    imagePath: string
}

export default class SelectImageModal extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            formFile: null,
            imagePath: this.props.imagePath
        }
    }

    render() {
        return (
            <div>
                <Modal show onHide={() => this.props.closeModal()}>
                    <Modal.Header closeButton className="Header-Data">
                        <Modal.Title>Выбор изображения</Modal.Title>
                    </Modal.Header>
                    <Modal.Body className="Form">
                        <div className="User-Info Form-Element">
                            <img src={this.state.imagePath} alt=""/>
                        </div>
                        <Form.Control type="file"
                                      className="Form-Element"
                                      accept='image/png, image/jpeg'
                            // value={this.state.formFile}
                                      onChange={async (e) => {
                                          const files = (e.target as HTMLInputElement).files;
                                          this.setState({formFile: files})

                                          const path = await DataProvider.createFile(WorkspaceManager.getWorkspace() ?? "", files![0])
                                          const file = DataProvider.getFilePath(path)

                                          this.setState({imagePath: file})
                                      }}/>
                        <button className="btn Primary-Button Form-Element"
                                onClick={() => {
                                    this.props.changeImage(this.state.imagePath)
                                    this.props.closeModal()
                                }}>
                            Сохранить
                        </button>
                    </Modal.Body>
                </Modal>
            </div>
        );
    }

}