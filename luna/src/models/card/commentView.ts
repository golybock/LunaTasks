import {Guid} from "guid-typescript";

export default class CommentView {
    constructor(id: number, userId: Guid, comment: string, attachmentUrl: string) {
        this.id = id;
        this.userId = userId;
        this.comment = comment;
        this.attachmentUrl = attachmentUrl;
    }

    public id: number;
    public userId: Guid;
    public comment: string;
    public attachmentUrl: string;
}