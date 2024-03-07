using System.ComponentModel.DataAnnotations;

namespace Luna.Models.Workspace.Blank.Workspace;

public class WorkspaceBlank
{
	[Required]
	public String Name { get; set; } = null!;
}