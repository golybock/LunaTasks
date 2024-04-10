export default interface IWorkspaceView{
    id: string;
    name : string;
    createdTimestamp: string;
    createdUserId: string;
    workspaceUsersDomains: number[];
}