import ProviderBase from "../providerBase";
import axios from "axios";
import PageView from "../../models/page/pageView";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IStatusBlank from "../../models/card/blank/statusBlank";
import IPageBlank from "../../models/page/pageBlank";

export default class PageProvider extends ProviderBase {

    static async getPages(workspaceId: string): Promise<Array<PageView>> {

        let url = this.baseAddress + "/Page/GetWorkspacePages?workspaceId=" + workspaceId;

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static async getPage(pageId: string): Promise<PageView | null>{
        let url = this.baseAddress + "/Page/GetPage?id=" + pageId;

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return null;
            })
            .catch(() => {
                return null;
            });
    }

    static async createPage(pageBlank: IPageBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Page/CreatePage";

        let token = AuthWrapper.user();

        return await axios.post(url, pageBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updatePage(id: string, pageBlank: IPageBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Page/UpdatePage?id=" + id;

        let token = AuthWrapper.user();

        return await axios.put(url, pageBlank,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deletePage(id: string) : Promise<boolean>{

        let url = this.baseAddress + "/Page/DeletePage?id=" + id;

        let token = AuthWrapper.user();

        return await axios.delete(url,{headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }
}