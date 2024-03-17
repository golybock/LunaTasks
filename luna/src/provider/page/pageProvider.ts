import ProviderBase from "../providerBase";
import axios from "axios";
import {Guid} from "guid-typescript";
import PageView from "../../models/page/pageView";

export default class PageProvider extends ProviderBase {

    static async getPages(workspaceId: Guid): Promise<PageView[]> {

        let url = this.baseAddress + "/Page/GetWorkspacePages?workspaceId=" + workspaceId;

        let token = localStorage.getItem("token");

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

    static async getPage(pageId: Guid): Promise<PageView | null>{
        let url = this.baseAddress + "/Page/GetPage?id=" + pageId;

        let token = localStorage.getItem("token");

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
}