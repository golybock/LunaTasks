import TypeView from "./typeView";
import CommentView from "./commentView";
import TagView from "./tagView";
import StatusView from "./statusView";
import IUserView from "../../user/userView";

export default interface ICardView {
    id: string;
    header: string;
    content: string | null;
    description: string | null;
    cardType: TypeView;
    createdUserId: string;
    createdTimestamp: string;
    deadline: string | null;
    previousCard: ICardView | null;
    comments: CommentView[];
    cardTags: TagView[];
    users: IUserView[];
    status: StatusView;
}