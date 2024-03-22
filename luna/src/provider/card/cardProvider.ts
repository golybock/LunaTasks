import ProviderBase from "../providerBase";
import axios from "axios";
import {Guid} from "guid-typescript";
import PageView from "../../models/page/pageView";
import AuthProvider from "../auth/authProvider";
import {AuthWrapper} from "../../auth/AuthWrapper";
import CardView from "../../models/card/view/cardView";
import CardBlank from "../../models/card/blank/cardBlank";

export default class CardProvider extends ProviderBase {

    static async getCard(cardId: string): Promise<CardView | null> {

        let url = this.baseAddress + "/Card/GetCard?id=" + cardId;

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

    static async createCard(cardBlank: CardBlank): Promise<boolean> {

        let url = this.baseAddress + "/Card/CreateCard";

        let token = AuthWrapper.user();

        return await axios.post(url, cardBlank, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateCard(id: string, cardBlank: CardBlank): Promise<boolean> {

        let url = this.baseAddress + "/Card/UpdateCard?id=" + id;

        let token = AuthWrapper.user();

        return await axios.post(url, cardBlank, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }
}