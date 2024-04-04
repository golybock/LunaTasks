import ProviderBase from "../providerBase";
import axios from "axios";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IOption from "../../models/tools/IOption";
import StatusView from "../../models/card/view/statusView";
import IStatusBlank from "../../models/card/blank/statusBlank";
import {WorkspaceManager} from "../../tools/WorkspaceManager";

export default class StatusProvider extends ProviderBase {

    static async getStatuses() : Promise<Array<StatusView>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Status/GetStatuses?workspaceId=" + workspaceId;

        let token = AuthWrapper.user();

        return await axios.get(url,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if(res.status == 200){
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async getStatus(id: string) : Promise<StatusView>{

        let url = this.baseAddress + "/Status/GetStatus?statusId=" + id;

        let token = AuthWrapper.user();

        return await axios.get(url,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if(res.status == 200){
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async getStatusesOptions() : Promise<Array<IOption>>{

        let url = this.baseAddress + "/Status/GetStatuses?workspaceId=" + WorkspaceManager.getWorkspace();

        let token = AuthWrapper.user();

        return await axios.get(url,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if(res.status == 200){
                    return this.mapToOption(res.data);
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async createStatus(statusBlank: IStatusBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Status/CreateStatus";

        let token = AuthWrapper.user();

        statusBlank.hexColor = statusBlank.hexColor.replace("#", "");

        return await axios.post(url, statusBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateStatus(id: string, statusBlank: IStatusBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Status/UpdateStatus?id=" + id;

        let token = AuthWrapper.user();

        return await axios.put(url, statusBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteStatus(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Status/DeleteStatus?id=" + id;

        let token = AuthWrapper.user();

        return await axios.delete(url,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    private static mapToOption(data: any[]): IOption[]{
        return data.map(o => {return{label: o.name, value: o.id}});
    }
}