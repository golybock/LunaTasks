import TypeView from "./typeView";
import CommentView from "./commentView";
import TagView from "./tagView";
import StatusView from "./statusView";

export default interface CardView {
    id: string;
    header: string;
    content: string | null;
    description: string | null;
    cardType: TypeView;
    createdUserId: string;
    createdTimestamp: string;
    deadline: string | null;
    previousCard: CardView | null;
    comments: CommentView[];
    cardTags: TagView[];
    users: string[];
    statuses: StatusView[];
}