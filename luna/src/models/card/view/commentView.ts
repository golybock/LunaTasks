export default class CommentView {
    constructor(id: number, userId: string, comment: string, attachmentUrl: string) {
        this.id = id;
        this.userId = userId;
        this.comment = comment;
        this.attachmentUrl = attachmentUrl;
    }

    public id: number;
    public userId: string;
    public comment: string;
    public attachmentUrl: string;
}