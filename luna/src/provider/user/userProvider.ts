import ProviderBase from "../providerBase";
import axios from "axios";
import IUserView from "../../models/user/userView";

export default class UserProvider extends ProviderBase {
    static async getMe(): Promise<IUserView | null> {

        let url = this.baseAddress + "/Users/GetMe";

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