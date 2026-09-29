namespace ProjectManager.Application.Features.Comments.Queries.GetCommentsQuery
{
	public sealed record CommentResponse(string Content, Guid TaskId);
}