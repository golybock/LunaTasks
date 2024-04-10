export default interface ICardBlank {
    header: string;
    content: string | null;
    description: string | null;
    cardTypeId: string;
    pageId: string | null; // only for create
    deadline: string | null;
    previousCardId: string | null;
    tagIds: string[];
    userIds: string[];
    statusId: string;
}