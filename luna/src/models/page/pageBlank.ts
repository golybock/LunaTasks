import {Guid} from "guid-typescript";

export default interface IPageBlank{
    name : string;
    description: string;
    headerImage: string;
    workspaceId: Guid;
}