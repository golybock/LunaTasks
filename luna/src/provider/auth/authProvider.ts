import ProviderBase from "../providerBase";
import axios from "axios";
import ISignInBlank from "../../models/auth/signInBlank";

export default class AuthProvider extends ProviderBase{

    static async signIn(email: string, password: string): Promise<boolean> {

        let url = this.baseAddress + "/Auth/SignIn";

        let blank: ISignInBlank = {email, password};

        return await axios.post(url, blank)
            .then(async res => {

                 if(res.status === 200){
                     localStorage.setItem("token", res.data)
                     return true;
                 }

                 return false;
            })
            .catch(() => {
                return false;
            });
    }

    // todo replace to signupblank
    static async signUp(UserBlank: ISignInBlank): Promise<boolean> {

        let url = this.baseAddress + "/Auth/SignUp";

        return await axios.post(url, UserBlank)
            .then(async res => {
                if(res.status === 200){
                    localStorage.setItem("token", res.data)
                    return true;
                }

                return false;
            })
            .catch(() => {
                return false;
            });
    }

    static async signOut(): Promise<boolean> {

        let url = this.baseAddress + "/Auth/SignOut";

        return await axios.post(url)
            .then(async res => {
                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

}