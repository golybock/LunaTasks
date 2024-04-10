import ProviderBase from "../providerBase";
import IOption from "../../models/tools/IOption";
import TypeView from "../../models/card/view/ITypeView";
import ITypeBlank from "../../models/card/blank/ITypeBlank";
import {mapToOption} from "../../tools/Mapper";

export default class TypeProvider extends ProviderBase {

    static async getTypes() : Promise<Array<TypeView>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Type/GetTypes?workspaceId=" + workspaceId;

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

    static async getType(id: string) : Promise<TypeView>{

        let url = this.baseAddress + "/Type/GetType?id=" + id;

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

    static async getTypesOptions() : Promise<Array<IOption>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Type/GetTypes?workspaceId=" + workspaceId;

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

    static async createType(typeBlank: ITypeBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Type/CreateType";

        typeBlank.hexColor = typeBlank.hexColor.replace("#", "");

        return await this.post(url, typeBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateType(id: string, typeBlank: ITypeBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Type/UpdateType?id=" + id;

        return await this.put(url, typeBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteType(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Type/DeleteType?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }
}