import CardView from "../card/view/cardView";

export default interface IPageView{
    id: string;
    name : string;
    description: string;
    headerImage: string;
    createdTimestamp: string;
    cards: CardView[];
}