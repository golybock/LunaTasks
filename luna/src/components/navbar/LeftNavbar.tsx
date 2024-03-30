import React from "react";
import {NavLink, Outlet} from "react-router-dom";
import "./Navbar.css"
import WorkspaceView from "../../models/workspace/workspaceView";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import {Dropdown} from "react-bootstrap";
import PageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import MenuItem from "../../models/navigation/menuItem";
import {AuthWrapper} from "../../auth/AuthWrapper";

interface IProps {
}

interface IState {
    selectedWorkspaceId: string;
    selectedPageId: string | null;
    workspaces: WorkspaceView[];
    pages: PageView[];
    menuItems: MenuItem[];
}

export class LeftNavbar extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            selectedWorkspaceId: "",
            selectedPageId: "",
            workspaces: [],
            pages: [],
            menuItems: this.defaultMenuItems
        }

    }

    defaultMenuItems = [
        {
            href: "/",
            title: "Home",
            image: "/icons/home.svg"
        },
        {
            href: "/account",
            title: "Account",
            image: "/icons/account.svg"
        },
        {
            href: "/about",
            title: "About",
            image: "/icons/about.svg"
        }
    ];

    async componentDidMount() {
        let workspaces = await WorkspaceProvider.getWorkspaces();

        this.setState({workspaces: workspaces});

        await this.selectWorkspace(localStorage.getItem("workspaceId"))
    }

    async selectWorkspace(id: string | null) {

        if (id == null) {
            this.setState({selectedWorkspaceId: this.state.workspaces[0]?.id});

            if (this.state.selectedWorkspaceId?.length > 0) {
                localStorage.setItem("workspaceId", this.state.selectedWorkspaceId);
            }

            return;
        }

        this.setState({selectedWorkspaceId: id});

        if (this.state.selectedWorkspaceId?.length > 0) {
            localStorage.setItem("workspaceId", this.state.selectedWorkspaceId);
        }

        let pages = await PageProvider.getPages(this.state.selectedWorkspaceId);

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
        this.setState({selectedPageId: pages[0]?.id})
    }

    render() {
        return (
            <div className="Navbar">
                <aside className="Navbar-Left">
                    <div>
                        {this.state.workspaces && (
                            <Dropdown data-bs-theme="dark"
                                // disabled={this.state.workspaces.length == 1}
                            >

                                {this.state.selectedWorkspaceId && (
                                    <Dropdown.Toggle variant="outline-secondary" className="Workspace-Dropdown">
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
                                </Dropdown.Menu>

                            </Dropdown>
                        )}
                    </div>
                    <nav className="Navbar-List">
                        {this.state.selectedWorkspaceId && (
                            <>
                                {this.state.menuItems.map((item: MenuItem) => (
                                    <NavLink key={item.title} to={item.href} end={true} replace={true} className="Navbar-Item" >
                                        <div className="Navbar-List-Item">
                                            {item.image && (
                                                <img src={item.image} alt=""/>
                                            )}
                                            <label>{item.title}</label>
                                        </div>
                                    </NavLink>
                                ))}
                            </>
                        )}
                        <div className="Navbar-List-Item">
                            <img src={"/icons/signOut.svg"} alt=""/>
                            <label onClick={() => AuthWrapper.userSignOut()}>SignOut</label>
                        </div>
                    </nav>
                </aside>
                <main className="Navbar-Container-Content">
                    <Outlet/>
                </main>
            </div>
        );
    }
}
