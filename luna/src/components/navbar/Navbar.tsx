import React from "react";
import {NavLink, Outlet} from "react-router-dom";
import "./Navbar.css"

interface IProps {
    signOut: Function
}

interface IState {
}

export class Navbar extends React.Component<IProps, IState>{

    constructor(props: IProps) {
        super(props);

        this.state = {
            signOut: Function
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
