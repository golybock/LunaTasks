import ProviderBase from "../providerBase";
import axios from "axios";
import PageView from "../../models/page/pageView";
import {AuthWrapper} from "../../auth/AuthWrapper";

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
}