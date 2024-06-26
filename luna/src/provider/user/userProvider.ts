﻿import ProviderBase from "../providerBase";
import IUserView from "../../models/user/IUserView";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IOption from "../../models/tools/IOption";
import {mapToOption, mapToOptionUser} from "../../tools/Mapper";
import IUserBlank from "../../models/user/IUserBlank";

export default class UserProvider extends ProviderBase {
    static async getMe(): Promise<IUserView | null> {

        let url = this.baseAddress + "/Users/GetMe";

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                return null;
            })
            .catch(() => {
                return null;
            });
    }

    static async getUsers(): Promise<Array<IUserView>> {

        let url = this.baseAddress + "/Users/GetUsers";

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }

    static  async getUser(id: string) : Promise<IUserView | null>{
        let url = this.baseAddress + "/Users/GetUserById?id=" + id;

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

    static async updateUser(userBlank: IUserBlank): Promise<boolean> {

        let url = this.baseAddress + "/Users/UpdateUser";

        return await this.put(url, userBlank)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async getUsersOptions(): Promise<Array<IOption>> {

        let url = this.baseAddress + "/Users/GetUsers";

        return await this.get(url)
            .then(async res => {

                if (res.status === 200) {
                    return mapToOptionUser(res.data);
                }

                if(res.status == 401){
                    AuthWrapper.userSignOut();
                }

                return [];
            })
            .catch(() => {
                return [];
            });
    }
}