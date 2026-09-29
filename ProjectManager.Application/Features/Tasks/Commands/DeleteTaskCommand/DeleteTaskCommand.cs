using MediatR;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand
{
	public sealed record DeleteTaskCommand(Guid TaskId) : IRequest<Result>;
}
