import ProviderBase from "../providerBase";
import ICardView from "../../models/card/view/ICardView";
import ICardBlank from "../../models/card/blank/ICardBlank";
import {WorkspaceManager} from "../../tools/WorkspaceManager";

export default class CardProvider extends ProviderBase {

    static async getCards(pageId: string): Promise<Array<ICardView>> {

        let url = this.baseAddress + "/Card/GetCards?pageId=" + pageId;

        return await this.get(url)
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

    static async getCardsByUserIds(pageId: string, userIds: string[]): Promise<Array<ICardView>> {

        let url = this.baseAddress + "/Card/GetCards?pageId=" + pageId;

        userIds.forEach(i => {
            url += "&userIds" + i;
        })

        return await this.get(url)
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

    static async getCardsByWorkspace(): Promise<Array<ICardView>> {

        let url = this.baseAddress + "/Card/GetCardsByWorkspace?workspaceId=" + WorkspaceManager.getWorkspace();

        return await this.get(url)
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

    static async createCard(cardBlank: ICardBlank): Promise<boolean> {

        let url = this.baseAddress + "/Card/CreateCard";

        return await this.post(url, cardBlank)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateCard(id: string, cardBlank: ICardBlank): Promise<boolean> {

        let url = this.baseAddress + "/Card/UpdateCard?id=" + id;

        return await this.post(url, cardBlank)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }
}