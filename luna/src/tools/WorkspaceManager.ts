export class WorkspaceManager {
    static getWorkspace = () => localStorage.getItem("workspaceId");

    static setWorkspace = (workspaceId: string) => {
        localStorage.setItem("workspaceId", workspaceId)
    };
}