export default class CardBlank {
    constructor(
        header: string,
        content: string | null,
        description: string | null,
        cardTypeId: string,
        pageId: string | null,
        deadline: string | null,
        previousCardId: string | null
    ) {
        this.header = header;
        this.content = content;
        this.description = description;
        this.cardTypeId = cardTypeId;
        this.pageId = pageId;
        this.deadline = deadline;
        this.previousCardId = previousCardId;
    }

    public header: string;
    public content: string | null;
    public description: string | null;
    public cardTypeId: string;
    public pageId: string | null; // only for create
    public deadline: string| null;
    public previousCardId: string | null;
}