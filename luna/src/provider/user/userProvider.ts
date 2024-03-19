import ProviderBase from "../providerBase";
import axios from "axios";
import IUserView from "../../models/user/userView";
import {AuthWrapper} from "../../auth/AuthWrapper";

export default class UserProvider extends ProviderBase {
    static async getMe(): Promise<IUserView | null> {

        let url = this.baseAddress + "/Users/GetMe";

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