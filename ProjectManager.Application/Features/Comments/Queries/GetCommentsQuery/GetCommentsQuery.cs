using MediatR;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Comments.Queries.GetCommentsQuery
{
	public sealed record GetCommentsQuery(Guid TaskId) : IRequest<Result<List<CommentResponse>>>;
}
