import {Guid} from "guid-typescript";

export default interface IPageView{
    id: Guid;
    name : string;
    description: string;
    headerImage: string;
    createdTimestamp: string;
    cards: number[];
}