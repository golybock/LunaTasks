import ProviderBase from "../providerBase";
import ICardView from "../../models/card/view/ICardView";
import ICardBlank from "../../models/card/blank/ICardBlank";
import {WorkspaceManager} from "../../tools/WorkspaceManager";
import ICommentView from "../../models/card/view/ICommentView";
import ICommentBlank from "../../models/card/blank/ICommentBlank";

export default class CommentProvider extends ProviderBase {

    static async getComments(cardId: string): Promise<ICardView | null> {

        let url = this.baseAddress + "/Comment/GetComments?cardId=" + cardId;

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

    static async getMyComments(): Promise<boolean> {

        let url = this.baseAddress + "/Comment/GetUserComments";

        return await this.get(url)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async createComment(commentBlank: ICommentBlank): Promise<boolean> {

        let url = this.baseAddress + "/Comment/CreateComment";

        return await this.post(url, commentBlank)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async updateComment(id: string, commentBlank: ICommentBlank): Promise<boolean> {

        let url = this.baseAddress + "/Comment/UpdateComment?id=" + id;

        return await this.put(url, commentBlank)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }

    static async deleteComment(id: string): Promise<boolean> {

        let url = this.baseAddress + "/Comment/DeleteComment?id=" + id;

        return await this.delete(url)
            .then(async res => {

                return res.status === 200;
            })
            .catch(() => {
                return false;
            });
    }
}