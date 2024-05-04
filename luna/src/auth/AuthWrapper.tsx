import React from "react";
import {Navigate} from "react-router-dom";

export class AuthWrapper {
    static user = () => localStorage.getItem("user");

    static userAuthed = () => this.user() != null;

    static userSignIn = (token: string) => {
        localStorage.setItem("user", token)
        window.location.replace("/");
    };

    static userSignOut = () => {
        localStorage.clear();
        window.location.replace("/");
    }
}


export type ProtectedRouteProps = {
    outlet: JSX.Element;
};

export function ProtectedRoute({outlet}: ProtectedRouteProps) {
    if(AuthWrapper.userAuthed()) {
        return outlet;
    } else {
        return <Navigate to={{ pathname: "/signIn" }} />;
    }
}

export function NotAuthedRoute({outlet}: ProtectedRouteProps) {
    if(!AuthWrapper.userAuthed()) {
        return outlet;
    } else {
        return <Navigate to={{ pathname: "/" }} />;
    }
}


// export function PublicRoute({outlet}: ProtectedRouteProps) {
//     if(!AuthWrapper.userAuthed()) {
//         return outlet;
//     } else {
//         return <Navigate to={{ pathname: "/" }} />;
//     }
// }