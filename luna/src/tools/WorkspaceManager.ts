export class WorkspaceManager {
    static getWorkspace = () => localStorage.getItem("workspaceId");

    static workspaceSelected = (workspaceId: string) => {
        localStorage.setItem("workspaceId", workspaceId)
    };
}