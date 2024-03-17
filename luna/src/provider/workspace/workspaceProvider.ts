import ProviderBase from "../providerBase";
import axios from "axios";
import IWorkspaceView from "../../models/workspace/workspaceView";

export default class WorkspaceProvider extends ProviderBase{

    static async getWorkspaces(): Promise<IWorkspaceView[]> {

        let url = this.baseAddress + "/Workspace/GetWorkspaces";

        let token = localStorage.getItem("token");

        return await axios.get(url, { headers: {"Authorization" : `Bearer ${token}`} })
            .then(async res => {

                if(res.status === 200){
                    res.data
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }
}