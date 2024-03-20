import {Guid} from "guid-typescript";
import CardView from "../card/cardView";

export default interface IPageView{
    id: Guid;
    name : string;
    description: string;
    headerImage: string;
    createdTimestamp: string;
    cards: CardView[];
}