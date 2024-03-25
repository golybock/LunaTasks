import ProviderBase from "../providerBase";
import axios from "axios";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IOption from "../../models/tools/IOption";
import StatusView from "../../models/card/view/statusView";
import IStatusBlank from "../../models/card/blank/statusBlank";
import TagView from "../../models/card/view/tagView";
import ITagBlank from "../../models/card/blank/tagBlank";
import TypeView from "../../models/card/view/typeView";
import ITypeBlank from "../../models/card/blank/typeBlank";

export default class TypeProvider extends ProviderBase {

    static async getTypes() : Promise<Array<TypeView>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Type/GetTypes?workspaceId=" + workspaceId;

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

    static async getType(id: string) : Promise<TypeView>{

        let url = this.baseAddress + "/Type/GetType?id=" + id;

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

    static async getTypesOptions() : Promise<Array<IOption>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Type/GetTypes?workspaceId=" + workspaceId;

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

    static async createType(typeBlank: ITypeBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Type/CreateType";

        let token = AuthWrapper.user();

        typeBlank.hexColor = typeBlank.hexColor.replace("#", "");

        return await axios.post(url, typeBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateType(id: string, typeBlank: ITypeBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Type/UpdateType?id=" + id;

        let token = AuthWrapper.user();

        return await axios.put(url, typeBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteType(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Type/DeleteType?id=" + id;

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