import ProviderBase from "../providerBase";
import axios from "axios";
import IWorkspaceView from "../../models/workspace/workspaceView";
import {AuthWrapper} from "../../auth/AuthWrapper";
import Auth from "../../App";
import IUserView from "../../models/user/userView";

export default class WorkspaceProvider extends ProviderBase {

    static async getWorkspaces(): Promise<IWorkspaceView[]> {

        let url = this.baseAddress + "/Workspace/GetWorkspaces";

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                console.log(res)
            });
    }

    static async getCurrentWorkspace(): Promise<IWorkspaceView | null> {

        let url = this.baseAddress + "/Workspace/GetWorkspace?id=" + localStorage.getItem("workspaceId");

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                console.log(res)
            });
    }

    static async getWorkspaceUsers(workspaceId: string): Promise<Array<IUserView>>{
        let url = this.baseAddress + "/Workspace/GetWorkspaceUsers?workspaceId=" + workspaceId;

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                console.log(res)
            });
    }

    static async getWorkspace(id: string): Promise<IWorkspaceView | null> {

        let url = this.baseAddress + "/Workspace/GetWorkspace?id=" + id;

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                console.log(res)
            });
    }

    static async joinToWorkspace(workspaceId: string): Promise<string>{
        let url = this.baseAddress + "/Workspace/JoinToWorkspace?workspaceId=" + workspaceId;

        let token = AuthWrapper.user();

        return await axios.post(url, null, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return res.data;
            })
            .catch((res) => {
                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                return res.response.data;
            });
    }

}