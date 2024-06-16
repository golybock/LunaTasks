import ProviderBase from "../providerBase";
import IWorkspaceView from "../../models/workspace/IWorkspaceView";
import IUserView from "../../models/user/IUserView";
import IOption from "../../models/tools/IOption";
import IWorkspaceBlank from "../../models/workspace/IWorkspaceBlank";
import {WorkspaceManager} from "../../tools/WorkspaceManager";
import {mapToOption, mapToOptionUser} from "../../tools/Mapper";

export default class WorkspaceProvider extends ProviderBase {

    static async getWorkspaces(): Promise<IWorkspaceView[]> {

        let url = this.baseAddress + "/Workspace/GetWorkspaces";

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
            });
    }

    static async getUserWorkspacesAsync(): Promise<IWorkspaceView[]> {

        let url = this.baseAddress + "/Workspace/GetUserWorkspaces";

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
            });
    }

    static async getCurrentWorkspace(): Promise<IWorkspaceView | null> {

        let url = this.baseAddress + "/Workspace/GetWorkspace?id=" + WorkspaceManager.getWorkspace();

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
            });
    }

    static async getWorkspaceUsers(): Promise<Array<IUserView>>{
        let url = this.baseAddress + "/Workspace/GetWorkspaceUsers?workspaceId=" + WorkspaceManager.getWorkspace();

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
            });
    }

    static async createWorkspace(pageBlank: IWorkspaceBlank) : Promise<boolean>{

        let url = this.baseAddress + "/Workspace/CreateWorkspace";

        return await this.post(url, pageBlank)
            .then(async res => {

                return res.status == 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async getWorkspaceUsersOptions(workspaceId: string): Promise<Array<IOption>>{
        let url = this.baseAddress + "/Workspace/GetWorkspaceUsers?workspaceId=" + workspaceId;

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return mapToOptionUser (res.data);
                }

                return [];
            })
            .catch((res) => {
                return [];
            });
    }


    static async getWorkspace(id: string): Promise<IWorkspaceView | null> {

        let url = this.baseAddress + "/Workspace/GetWorkspace?id=" + id;

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return [];
            })
            .catch((res) => {
                console.log(res)
            });
    }

    static async joinToWorkspace(workspaceId: string): Promise<string>{
        let url = this.baseAddress + "/Workspace/JoinToWorkspace?workspaceId=" + workspaceId;

        return await this.post(url, null)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return res.data;
            })
            .catch((res) => {
                return res.response.data;
            });
    }

    static async deleteUserFromWorkspace(userId: string): Promise<boolean | string>{
        let url = this.baseAddress + "/Workspace/DeleteUserFromWorkspace?workspaceId=" + WorkspaceManager.getWorkspace() + "&userId=" + userId;

        return await this.delete(url)
            .then(async res => {
                return res.status === 200;
            })
            .catch((res) => {
                return res.response.data;
            });
    }
}