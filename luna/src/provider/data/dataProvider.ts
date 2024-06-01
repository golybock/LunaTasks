import ProviderBase from "../providerBase";


export default class DataProvider extends ProviderBase {
    static getFilePath(id: string): string {

        return this.baseAddress.replace("/api", "/") + id;
    }

    static async createFile(workspaceId: string, file: File): Promise<string> {

        let url = this.baseAddress.replace("/api", "") + "/CreateFile?workspaceId=" + workspaceId;

        const formData = new FormData()
        formData.append("userpic", file)

        return await this.postForm(url, formData)
            .then(async res => {

                return res.data
            })
            .catch(() => {
                return "";
            });
    }

    static async deleteFile(id: string): Promise<boolean> {

        let url = this.baseAddress + "/DeleteFile?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }
}