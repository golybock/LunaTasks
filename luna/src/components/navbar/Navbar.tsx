import React from "react";
import {NavLink, Outlet} from "react-router-dom";
import "./Navbar.css"
import WorkspaceView from "../../models/workspace/workspaceView";
import WorkspaceProvider from "../../provider/workspace/workspaceProvider";
import {Dropdown} from "react-bootstrap";
import PageView from "../../models/page/pageView";
import PageProvider from "../../provider/page/pageProvider";
import {Guid} from "guid-typescript";

interface IProps {
    signOut: Function
}

interface IState {
    selectedWorkspaceId: Guid;
    selectedPageId: Guid;
    workspaces: WorkspaceView[];
    pages: PageView[]
}

export class Navbar extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            selectedWorkspaceId: Guid.createEmpty(),
            selectedPageId: Guid.createEmpty(),
            workspaces: [],
            pages: []
        }
    }

    async componentDidMount() {
        let workspaces = await WorkspaceProvider.getWorkspaces();

        this.setState({workspaces: workspaces});
        this.setState({selectedWorkspaceId: workspaces[0].id})

        await this.workspaceChosen()
    }

    async workspaceChosen() {

        let pages = await PageProvider.getPages(this.state.selectedWorkspaceId);

        console.log(pages);

        pages.forEach(page => {
            this.MenuItems.push(
                {
                    href: "/page?id=" + page.id,
                    title: page.name,
                    img: ""
                }
            )
        })

        if(pages.length > 0){
            this.setState({pages: pages});
            this.setState({selectedPageId: pages[0].id})
        }
    }

    MenuItems = [
        {
            href: "/",
            title: "Home",
            img: "/icons/home.svg"
        },
        {
            href: "/account",
            title: "Account",
            img: "/icons/account.svg"
        },
        {
            href: "/about",
            title: "About",
            img: "/icons/about.svg"
        }
    ];

    render() {
        return (
            <div className="Navbar-Container">
                {/*<header*/}
                {/*    className="bg-gray-200 text-black sticky top-0 h-14 flex justify-center items-center font-semibold uppercase">*/}
                {/*    Cloudinary Actions*/}
                {/*</header>*/}
                <div className="Navbar-Left">
                    <aside className="Navbar-Items-Container">
                        <div>
                            {this.state.workspaces && (
                                <Dropdown data-bs-theme="dark"
                                    // disabled={this.state.workspaces.length == 1}
                                >

                                    <Dropdown.Toggle variant="outline-secondary" className="Workspace-Dropdown">
                                        {this.state.workspaces.find(c => c.id == this.state.selectedWorkspaceId)?.name ?? "Workspace"}
                                    </Dropdown.Toggle>

                                    <Dropdown.Menu className="Workspace-Dropdown-Menu">
                                        {this.state.workspaces.map((workspace) => (
                                            <Dropdown.Item key={workspace.id.toString()}
                                                           onClick={
                                                               async (e) => {
                                                                   this.setState({selectedWorkspaceId: workspace.id})
                                                                   await this.workspaceChosen()
                                                               }
                                                           }
                                                           className="Workspace-Dropdown-Item">{workspace.name}</Dropdown.Item>
                                        ))}
                                    </Dropdown.Menu>

                                </Dropdown>
                            )}
                        </div>
                        <nav className="Navbar-List">
                            {this.MenuItems.map(({href, title, img}) => (
                                <NavLink key={title} to={href} className="Navbar-Item">
                                    <div className="Navbar-List-Item">
                                        {img != null && (
                                            <img src={img}/>
                                        )}
                                        <label>{title}</label>
                                    </div>
                                </NavLink>
                            ))}
                            <div className="Navbar-List-Item">
                                <label onClick={() => this.props.signOut()}>SignOut</label>
                            </div>
                        </nav>
                    </aside>
                    <main className="Navbar-Container-Content">
                        <Outlet/>
                    </main>
                </div>
                {/*<footer></footer>*/}
            </div>
        );
    }
}
