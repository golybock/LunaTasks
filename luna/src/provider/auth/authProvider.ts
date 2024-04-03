import ProviderBase from "../providerBase";
import axios from "axios";
import ISignInBlank from "../../models/auth/signInBlank";
import {AuthWrapper} from "../../auth/AuthWrapper";
import ISignUpBlank from "../../models/auth/signUpBlank";

export default class AuthProvider extends ProviderBase{

    static async signIn(email: string, password: string): Promise<boolean | string> {

        let url = this.baseAddress + "/Auth/SignIn";

        let blank: ISignInBlank = {email, password};

        return await axios.post(url, blank)
            .then(async res => {

                 if(res.status === 200){
                     AuthWrapper.userSignIn(res.data);
                     return true;
                 }

                 return false;
            })
            .catch((res) => {

                if(res.status == 401){
                    return res.data;
                }

                return false;
            });
    }

    // todo replace to signupblank
    static async signUp(UserBlank: ISignUpBlank): Promise<boolean | string> {

        let url = this.baseAddress + "/Auth/SignUp";

        return await axios.post(url, UserBlank)
            .then(async res => {
                if(res.status === 200){
                    AuthWrapper.userSignIn(res.data);
                    return true;
                }

                return false;
            })
            .catch((res) => {

                if(res.status == 400){
                    return res.data;
                }

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