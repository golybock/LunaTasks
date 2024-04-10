import ITypeView from "./ITypeView";
import ICommentView from "./ICommentView";
import ITagView from "./ITagView";
import IStatusView from "./IStatusView";
import IUserView from "../../user/IUserView";

export default interface ICardView {
    id: string;
    header: string;
    content: string | null;
    description: string | null;
    cardType: ITypeView;
    createdUserId: string;
    createdTimestamp: string;
    deadline: string | null;
    previousCard: ICardView | null;
    comments: ICommentView[];
    cardTags: ITagView[];
    users: IUserView[];
    status: IStatusView;
}