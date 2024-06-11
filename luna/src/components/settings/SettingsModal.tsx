import React from "react";
import {Modal} from "react-bootstrap";
import "./SettingsModal.css";
import SettingsMenuItem from "../../models/navigation/SettingsMenuItem";
import SettingsSections from "../../tools/SettingsSections";
import SettingsTags from "./content/SettingsTags";
import SettingsStatuses from "./content/SettingsStatuses";
import SettingsTypes from "./content/SettingsTypes";
import SettingsUsers from "./content/SettingsUsers";
import SettingsThemes from "./content/SettingsThemes";
import SettingsPages from "./content/SettingsPages";


interface IProps {
    show: boolean
}

interface IState {
    menuItems: SettingsMenuItem[];
    selectedMenuItem: SettingsSections;
}

export default class SettingsModal extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);

        this.state = {
            menuItems: this.defaultMenuItems,
            selectedMenuItem: SettingsSections.Tags
        }
    }

    defaultMenuItems = [
        {
            href: SettingsSections.Tags,
            title: "Tags",
            image: null
        },
        {
            href: SettingsSections.Statuses,
            title: "Statuses",
            image: null
        },
        {
            href: SettingsSections.Types,
            title: "Types",
            image: null
        },
        {
            href: SettingsSections.Users,
            title: "Users",
            image: null
        },
        {
            href: SettingsSections.Pages,
            title: "Pages",
            image: null
        },
        // {
        //     href: SettingsSections.Theme,
        //     title: "Theme",
        //     image: null
        // },
    ];

    close(){
        window.location.replace("/")
    }

    render() {
        return (
            <>
                {this.props.show && (
                    <Modal show dialogClassName="Settings-Modal" onHide={() => this.close()}>

                        <Modal.Header closeButton>
                            <Modal.Title>Settings</Modal.Title>
                        </Modal.Header>

                        <div className="Settings-Modal-Body">
                            <div className="Settings-Nav-Panel">
                                <aside className="Settings-Navbar-Left">
                                    <nav>
                                        {this.state.menuItems && (
                                            <div>
                                                {this.state.menuItems.map((item: SettingsMenuItem) => (
                                                    <div key={item.title} className="Navbar-Item">
                                                        <div className="Navbar-List-Item"
                                                        onClick={() => this.setState({selectedMenuItem: item.href})}>
                                                            {item.image && (
                                                                <img src={item.image} alt=""/>
                                                            )}
                                                            <label>{item.title}</label>
                                                        </div>
                                                    </div>
                                                ))}
                                            </div>
                                        )}
                                    </nav>
                                </aside>
                            </div>
                            <div className="Settings-Content">
                                {this.state.selectedMenuItem == SettingsSections.Tags && (
                                    <SettingsTags/>
                                )}

                                {this.state.selectedMenuItem == SettingsSections.Statuses && (
                                    <SettingsStatuses/>
                                )}

                                {this.state.selectedMenuItem == SettingsSections.Types && (
                                    <SettingsTypes/>
                                )}

                                {this.state.selectedMenuItem == SettingsSections.Users && (
                                    <SettingsUsers/>
                                )}

                                {this.state.selectedMenuItem == SettingsSections.Theme && (
                                    <SettingsThemes/>
                                )}

                                {this.state.selectedMenuItem == SettingsSections.Pages && (
                                    <SettingsPages/>
                                )}
                            </div>
                        </div>

                    </Modal>
                )}
            </>
        );
    }
}