﻿using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Type;

public interface ITypeRepository
{
	public Task<IEnumerable<TypeDatabase>> GetTypesAsync(Guid workspaceId);

	public Task<TypeDatabase?> GetTypeAsync(Guid workspaceId, Guid typeId);

	public Task<TypeDatabase?> GetTypeAsync(Guid typeId);

	public Task<Boolean> CreateTypeAsync(TypeDatabase type);

	public Task<Boolean> UpdateTypeAsync(Guid id, TypeDatabase type);

	public Task<Boolean> DeleteTypeAsync(Guid id);
}