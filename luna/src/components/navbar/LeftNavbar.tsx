import React from "react";
import {NavLink, Outlet} from "react-router-dom";
import "./LeftNavbar.css"
import IWorkspaceView from "../../models/workspace/IWorkspaceView";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import {Dropdown} from "react-bootstrap";
import IPageView from "../../models/page/IPageView";
import PageProvider from "../../provider/page/pageProvider";
import MenuItem from "../../models/navigation/MenuItem";
import {AuthWrapper} from "../../auth/AuthWrapper";
import {WorkspaceManager} from "../../tools/WorkspaceManager";
import WorkspaceModal from "../account/modals/WorkspaceModal";

interface IProps {
}

interface IState {
    selectedWorkspaceId: string;
    selectedPageId: string | null;
    workspaces: IWorkspaceView[];
    pages: IPageView[];
    menuItems: MenuItem[];
    showWorkspaceModal: boolean;
    showSettingsModal: boolean;
}

export class LeftNavbar extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            selectedWorkspaceId: "",
            selectedPageId: null,
            workspaces: [],
            pages: [],
            menuItems: this.defaultMenuItems,
            showSettingsModal: false,
            showWorkspaceModal: false
        }

    }

    defaultMenuItems = [
        {
            href: "/",
            title: "Домашняя",
            image: "/icons/home.svg"
        },
        {
            href: "/account",
            title: "Аккаунт",
            image: "/icons/account.svg"
        },
        {
            href: "/settings",
            title: "Настройки",
            image: "/icons/settings.svg"
        },
        {
            href: "/statistic",
            title: "Статистика",
            image: "/icons/statistic.svg"
        }
    ];

    async componentDidMount() {
        let workspaces = await WorkspaceProvider.getUserWorkspacesAsync();

        this.setState({workspaces: workspaces});
        console.log(workspaces)

        let workspaceId = WorkspaceManager.getWorkspace();

        await this.selectWorkspace(workspaceId)
    }

    async selectWorkspace(id: string | null) {

        let id1 = id;

        //id empty
        if (id == null || id == "") {

            console.log("id empty")

            // set first available workspaceId
            if(this.state.workspaces.length){

                console.log("workspaces", this.state.workspaces)

                this.setState({selectedWorkspaceId: this.state.workspaces[0].id});

                WorkspaceManager.setWorkspace(this.state.workspaces[0].id)

                id1 = this.state.workspaces[0].id;
            }
        } else {
            // workspace not empty - set new workspaceid
            this.setState({selectedWorkspaceId: id});

            WorkspaceManager.setWorkspace(id)
        }

        // load pages
        const workspaceId = id1;

        console.log("workspaceId", workspaceId)

        if(workspaceId){
            let pages = await PageProvider.getPages(workspaceId);

            let pageMenuItems: MenuItem[] = [];

            if (pages.length > 0) {

                pages.forEach(page => {
                    pageMenuItems.push(
                        {
                            href: "/page/" + page.id,
                            title: page.name,
                            image: "/icons/page.svg"
                        }
                    )
                })
            }

            this.setState({menuItems: [...this.defaultMenuItems, ...pageMenuItems]})

            this.setState({pages: pages});
        }
    }

    openWorkspaceModal() {
        this.setState({showWorkspaceModal: true})
    }

    closeWorkspaceModal() {
        this.setState({showWorkspaceModal: false})
    }

    render() {
        return (
            <div className="Navbar">
                <aside className="Navbar-Left">
                    <div>
                        {this.state.workspaces && (
                            <Dropdown className="Workspace-Dropdown">

                                {this.state.selectedWorkspaceId && (
                                    <Dropdown.Toggle variant="outline-secondary">
                                        {this.state.workspaces.find(c => c.id == this.state.selectedWorkspaceId)?.name ?? "Workspace"}
                                    </Dropdown.Toggle>
                                )}

                                <Dropdown.Menu className="Workspace-Dropdown-Menu">
                                    {this.state.workspaces.map((workspace) => (
                                        <Dropdown.Item key={workspace.id.toString()}
                                                       onClick={
                                                           async (e) => {
                                                               console.log(workspace.id)
                                                               await this.selectWorkspace(workspace.id);
                                                           }
                                                       }
                                                       className="Workspace-Dropdown-Item">{workspace.name}</Dropdown.Item>
                                    ))}
                                    <Dropdown.Item key="create_workspace"
                                                   className="Workspace-Dropdown-Item">
                                        <button className="Outline-Button Workspace-Button" onClick={() => {
                                            this.openWorkspaceModal();
                                        }}>Создать</button>
                                    </Dropdown.Item>
                                </Dropdown.Menu>

                            </Dropdown>
                        )}
                    </div>
                    <nav className="Navbar-List">
                        {this.state.menuItems && (
                            <>
                                {this.state.menuItems.map((item: MenuItem) => (
                                    <NavLink key={item.title} to={item.href} end={true} replace={true}>
                                        <div className="Navbar-List-Item">
                                            {item.image && (
                                                <img src={item.image} alt="" className="Icon"/>
                                            )}
                                            <label>{item.title}</label>
                                        </div>
                                    </NavLink>
                                ))}
                            </>
                        )}
                    </nav>
                    <div className="Sign-Out" onClick={() => AuthWrapper.userSignOut()}>
                        <button className="btn btn-outline-secondary">
                            <img src={"/icons/signOut.svg"} alt="" width="30" height="30"/>
                            <label>Выйти</label>
                        </button>
                    </div>
                </aside>

                <main className="Navbar-Container-Content">
                    <Outlet/>
                    {this.state.showWorkspaceModal && (
                        <WorkspaceModal closeModal={() => this.closeWorkspaceModal()}/>
                    )}
                </main>

            </div>
        );
    }
}
