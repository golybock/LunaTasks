import ProviderBase from "../providerBase";
import axios from "axios";
import {AuthWrapper} from "../../auth/AuthWrapper";
import ICardView from "../../models/card/view/cardView";
import CardBlank from "../../models/card/blank/cardBlank";
import IOption from "../../models/tools/IOption";

export default class CardProvider extends ProviderBase {

    static async getCards(pageId: string): Promise<Array<ICardView>> {

        let url = this.baseAddress + "/Card/GetCards?pageId=" + pageId;

        let token = AuthWrapper.user();

        return await axios.get(url, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                if (res.status === 200) {
                    return res.data;
                }

                return null;
            })
            .catch(() => {
                return [];
            });
    }

    static async getCard(cardId: string): Promise<ICardView | null> {

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

    static async addTagToCard(cardId: string, tagId: string): Promise<boolean> {

        let url = this.baseAddress + "/Card/AddCardTag?cardId=" + cardId + "&tagId=" + tagId;

        let token = AuthWrapper.user();

        return await axios.post(url, null, {headers: {"Authorization": `Bearer ${token}`}})
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    private static mapToOption(data: any[]): IOption[]{
        return data.map(o => {return{label: o.name, value: o.id}});
    }
}