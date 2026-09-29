using MediatR;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Comments.Commands.DeleteCommentCommand
{
	public sealed record DeleteCommentCommand(Guid Id) : IRequest<Result>;
}