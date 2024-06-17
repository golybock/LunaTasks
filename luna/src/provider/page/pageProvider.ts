import ProviderBase from "../providerBase";
import IPageView from "../../models/page/IPageView";
import IPageBlank from "../../models/page/IPageBlank";
import NotificationManager from "../../tools/NotificationManager";

export default class PageProvider extends ProviderBase {

    static async getPages(workspaceId: string): Promise<Array<IPageView>> {

        let url = this.baseAddress + "/Page/GetWorkspacePages?workspaceId=" + workspaceId;

        return await this.get(url)
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

    static async getPageReport(pageId: string){
        let url = this.baseAddress + "/Card/GetCardsXlsx?pageId=" + pageId;

        return await this.getFile(url)
            .then(async res => {

                if (res.status === 200) {

                    const href = URL.createObjectURL(res.data);

                    const link = document.createElement('a');
                    link.href = href;
                    link.setAttribute('download', 'report.xlsx'); //or any other extension
                    document.body.appendChild(link);
                    link.click();

                    document.body.removeChild(link);
                    URL.revokeObjectURL(href);
                }

                NotificationManager.makeSuccess("File downloaded");

                return null;
            })
            .catch((e) => {
                console.log(e)
                NotificationManager.makeError("error");
                return null;
            });
    }

    static async getPage(pageId: string): Promise<IPageView | null>{
        let url = this.baseAddress + "/Page/GetPage?id=" + pageId;

        return await this.get(url)
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

        return await this.post(url, pageBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updatePage(id: string, pageBlank: IPageBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Page/UpdatePage?id=" + id;

        return await this.put(url, pageBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deletePage(id: string) : Promise<boolean | string>{

        let url = this.baseAddress + "/Page/DeletePage?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status == 200;
            })
            .catch((res) => {
                return res.response.data;
            });
    }

    static async trashPage(id: string) : Promise<boolean | string>{

        let url = this.baseAddress + "/Page/TrashPage?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status == 200;
            })
            .catch((res) => {
                return res.response.data;
            });
    }
}