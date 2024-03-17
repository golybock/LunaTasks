import {Guid} from "guid-typescript";

export default interface IWorkspaceView{
    id: Guid;
    name : string;
    createdTimestamp: string;
    createdUserId: string;
    workspaceUsersDomains: number[];
}