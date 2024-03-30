import ProviderBase from "../providerBase";
import axios from "axios";
import IWorkspaceView from "../../models/workspace/workspaceView";
import {AuthWrapper} from "../../auth/AuthWrapper";
import Auth from "../../App";

export default class WorkspaceProvider extends ProviderBase{

    static async getWorkspaces(): Promise<IWorkspaceView[]> {

        let url = this.baseAddress + "/Workspace/GetWorkspaces";

        let token = AuthWrapper.user();

        return await axios.get(url, { headers: {"Authorization" : `Bearer ${token}`} })
            .then(async res => {

                if(res.status === 200){
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                AuthWrapper.userSignOut();
            });
    }

    static async getCurrentWorkspace(): Promise<IWorkspaceView | null> {

        let url = this.baseAddress + "/Workspace/GetWorkspace?id=" + localStorage.getItem("workspaceId");

        let token = AuthWrapper.user();

        return await axios.get(url, { headers: {"Authorization" : `Bearer ${token}`} })
            .then(async res => {

                if(res.status === 200){
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                AuthWrapper.userSignOut();
            });
    }
}