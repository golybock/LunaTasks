import React from "react";
import "./SettingsTags.css"
import ITagView from "../../../models/card/view/ITagView";
import TagProvider from "../../../provider/card/tagProvider";
import Form from "react-bootstrap/Form";
import NotificationManager from "../../../tools/NotificationManager";
import TagModal from "../modals/TagModal";

interface IProps {

}

interface IState {
    tags: ITagView[],
    showTagsModal: boolean,
}

export default class SettingsTags extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            tags: [],
            showTagsModal: false
        }
    }

    async componentDidMount() {
        const tags = await TagProvider.getTags();
        this.setState({tags: tags});
    }

    showTagModal() {
        this.setState({showTagsModal: true})
    }

    async closeTagModal() {
        this.setState({showTagsModal: false})

        const tags = await TagProvider.getTags();
        this.setState({tags: tags});
    }

    async deleteTag(id: string) {
        const res = await TagProvider.deleteTag(id);

        if (res) {
            const tags = await TagProvider.getTags();
            this.setState({tags: tags});
        } else {
            NotificationManager.makeError("Error")
        }
    }

    render() {
        return (
            <div className="Settings-Tags">
                <div className="Item-Block" id="tags">
                    <div className="Item-Header">
                        <h4>Теги</h4>
                        <button className="btn Outline-Button" onClick={() => this.showTagModal()}>+
                        </button>
                    </div>
                    <hr/>
                    {this.state.tags ?
                        (
                            this.state.tags.map((item) => {
                                return (<div className="Item" key={item.id}>
                                    <label>{item.name}</label>
                                    <div className="row">
                                        <Form.Control disabled type="color" value={item.color}/>
                                        <button className="btn Outline-Button"
                                                onClick={() => this.deleteTag(item.id)}>-
                                        </button>
                                    </div>
                                </div>)
                            })
                        ) :
                        <div>Нет элементов</div>
                    }
                </div>
                    {this.state.showTagsModal && (
                        <TagModal closeModal={() => this.closeTagModal()}/>
                    )}
            </div>
        );
    }
}
