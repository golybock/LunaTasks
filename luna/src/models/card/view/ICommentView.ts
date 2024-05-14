import IUserView from "../../user/IUserView";

export default interface ICommentView {
    id: number;
    userId: string;
    user: IUserView;
    comment: string;
    attachmentUrl: string;
}