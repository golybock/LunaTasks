import axios from "axios";
import {AuthWrapper} from "../auth/AuthWrapper";

export default class ProviderBase {
    // todo add to config
    static baseAddress = "http://localhost:7000/api";

    constructor() {
        axios.defaults.withCredentials = true
    }

    protected static async get(url: string) {

        let token = AuthWrapper.user();

        const res = await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}});

        if (res.status == 401) {
            AuthWrapper.userSignOut();
        }

        return res;
    }

    protected static async post(url: string, data: any) {

        let token = AuthWrapper.user();

        const res = await axios.post(url, data, {headers: {"Authorization": `Bearer ${token}`}});

        if (res.status == 401) {
            AuthWrapper.userSignOut();
        }

        return res;
    }

    protected static async put(url: string, data: any) {

        let token = AuthWrapper.user();

        const res = await axios.put(url, data, {headers: {"Authorization": `Bearer ${token}`}});

        if (res.status == 401) {
            AuthWrapper.userSignOut();
        }

        return res;
    }

    protected static async delete(url: string) {

        let token = AuthWrapper.user();

        const res = await axios.delete(url, {headers: {"Authorization": `Bearer ${token}`}});

        if (res.status == 401) {
            AuthWrapper.userSignOut();
        }

        return res;
    }
}