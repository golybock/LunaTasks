import ProviderBase from "../providerBase";
import axios from "axios";
import IUserView from "../../models/user/userView";
import {AuthWrapper} from "../../auth/AuthWrapper";
import IOption from "../../models/tools/IOption";

export default class UserProvider extends ProviderBase {
    static async getMe(): Promise<IUserView | null> {

        let url = this.baseAddress + "/Users/GetMe";

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
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

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
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

    static async getUsersOptions(): Promise<Array<IOption>> {

        let url = this.baseAddress + "/Users/GetUsers";

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return this.mapToOption(res.data);
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

    private static mapToOption(data: any[]): IOption[]{
        return data.map(o => {return{label: o.username, value: o.id}});
    }
}