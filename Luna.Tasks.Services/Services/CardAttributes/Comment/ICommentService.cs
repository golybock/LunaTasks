﻿using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Tasks.Services.Services.CardAttributes.Comment;

public interface ICommentService
{
	public Task<IEnumerable<CommentView>> GetCommentsAsync(Guid cardId);

	public Task<IEnumerable<CommentView>> GetUserCommentsAsync(Guid userId);

	public Task<CommentView?> GetCommentAsync(Int32 commentId);

	public Task<Boolean> CreateCommentAsync(CommentBlank comment, Guid userId);

	public Task<Boolean> UpdateCommentAsync(Int32 id, CommentBlank comment, Guid userId);

	public Task<Boolean> DeleteCommentAsync(Int32 id);

	public Task<Boolean> DeleteCardCommentsAsync(Guid cardId);

	public Task<Boolean> DeleteUserCommentsAsync(Guid userId);
}