﻿using Luna.Tools.Enums;

namespace Luna.Models.Data.Domain.Data;

public class FileDomain
{
	public Guid Id { get; set; }

	public String Path { get; set; } = null!;

	public FileType FileType { get; set; }

	public Guid WorkspaceId { get; set; }

	public Guid UploadedByUserId { get; set; }

	public DateTime UploadedDate { get; set; }

	public Boolean Deleted { get; set; }
}