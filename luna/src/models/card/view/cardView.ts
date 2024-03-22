import TypeView from "./typeView";
import CommentView from "./commentView";
import TagView from "./tagView";
import StatusView from "./statusView";

export default class CardView {

    constructor(
        id: string,
        header: string,
        content: string | null,
        description: string | null,
        cardType: TypeView,
        createdUserId: string,
        createdTimestamp: string,
        deadline: string | null,
        previousCard: CardView | null,
        comments: CommentView[],
        cardTags: TagView[],
        users: string[],
        statuses: StatusView[]
    ) {
        this.id = id;
        this.header = header;
        this.content = content;
        this.description = description;
        this.cardType = cardType;
        this.createdUserId = createdUserId;
        this.createdTimestamp = createdTimestamp;
        this.deadline = deadline;
        this.previousCard = previousCard;
        this.comments = comments;
        this.cardTags = cardTags;
        this.users = users;
        this.statuses = statuses;
    }

    public id: string;
    public header: string;
    public content: string | null;
    public description: string | null;
    public cardType: TypeView;
    public createdUserId: string;
    public createdTimestamp: string;
    public deadline: string | null;
    public previousCard: CardView | null;
    public comments: CommentView[];
    public cardTags: TagView[];
    public users: string[];
    public statuses: StatusView[];
}