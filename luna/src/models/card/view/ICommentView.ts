import IUserView from "../../user/IUserView";

export default interface ICommentView {
    id: string;
    userId: string;
    user?: IUserView;
    comment: string;
    attachmentUrl: string;
}