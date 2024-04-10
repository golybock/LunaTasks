import ProviderBase from "../providerBase";
import IOption from "../../models/tools/IOption";
import IStatusView from "../../models/card/view/IStatusView";
import IStatusBlank from "../../models/card/blank/IStatusBlank";
import {WorkspaceManager} from "../../tools/WorkspaceManager";
import {mapToOption} from "../../tools/Mapper";

export default class StatusProvider extends ProviderBase {

    static async getStatuses() : Promise<Array<IStatusView>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Status/GetStatuses?workspaceId=" + workspaceId;

        return await this.get(url)
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

    static async getStatus(id: string) : Promise<IStatusView>{

        let url = this.baseAddress + "/Status/GetStatus?statusId=" + id;

        return await this.get(url)
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

        return await this.get(url)
            .then(async res => {

                if(res.status == 200){
                    return mapToOption(res.data);
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async createStatus(statusBlank: IStatusBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Status/CreateStatus";

        statusBlank.hexColor = statusBlank.hexColor.replace("#", "");

        return await this.post(url, statusBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateStatus(id: string, statusBlank: IStatusBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Status/UpdateStatus?id=" + id;

        return await this.put(url, statusBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteStatus(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Status/DeleteStatus?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }
}