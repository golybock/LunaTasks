import ProviderBase from "../providerBase";
import IOption from "../../models/tools/IOption";
import ITagView from "../../models/card/view/ITagView";
import ITagBlank from "../../models/card/blank/ITagBlank";
import {mapToOption} from "../../tools/Mapper";

export default class TagProvider extends ProviderBase {

    static async getTags() : Promise<Array<ITagView>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Tag/GetTags?workspaceId=" + workspaceId;

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

    static async getTag(id: string) : Promise<ITagView>{

        let url = this.baseAddress + "/Tag/GetTag?id=" + id;

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

    static async getTagsOptions() : Promise<Array<IOption>>{

        const workspaceId = localStorage.getItem("workspaceId")

        let url = this.baseAddress + "/Tag/GetTags?workspaceId=" + workspaceId;

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

    static async createTag(tagBlank: ITagBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Tag/CreateTag";

        tagBlank.hexColor = tagBlank.hexColor.replace("#", "");

        return await this.post(url, tagBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateTag(id: string, tagBlank: ITagBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Tag/UpdateTag?id=" + id;

        return await this.put(url, tagBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteTag(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Tag/DeleteTag?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }
}