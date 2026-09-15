using MediatR;

namespace ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand
{
	public sealed record DeleteTaskCommand(Guid TaskId) : IRequest;
}
