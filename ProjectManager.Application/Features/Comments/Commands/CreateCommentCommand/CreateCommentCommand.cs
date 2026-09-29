using MediatR;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Comments.Commands.CreateCommentCommand
{
	public sealed record CreateCommentCommand(Guid TaskId, string Content) : IRequest<Result<Guid>>;
}
