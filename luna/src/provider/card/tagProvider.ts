import ProviderBase from "../providerBase";
import axios from "axios";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IOption from "../../models/tools/IOption";
import StatusView from "../../models/card/view/statusView";
import IStatusBlank from "../../models/card/blank/statusBlank";
import TagView from "../../models/card/view/tagView";
import ITagBlank from "../../models/card/blank/tagBlank";

export default class TagProvider extends ProviderBase {

    static async getTags() : Promise<Array<TagView>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Tag/GetTags?workspaceId=" + workspaceId;

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

    static async getTag(id: string) : Promise<TagView>{

        let url = this.baseAddress + "/Tag/GetTag?id=" + id;

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

    static async getTagsOptions() : Promise<Array<IOption>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Tag/GetTags?workspaceId=" + workspaceId;

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

    static async createTag(tagBlank: ITagBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Tag/CreateTag";

        let token = AuthWrapper.user();

        tagBlank.hexColor = tagBlank.hexColor.replace("#", "");

        return await axios.post(url, tagBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateTag(id: string, tagBlank: ITagBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Tag/UpdateTag?id=" + id;

        let token = AuthWrapper.user();

        return await axios.put(url, tagBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteTag(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Tag/DeleteTag?id=" + id;

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