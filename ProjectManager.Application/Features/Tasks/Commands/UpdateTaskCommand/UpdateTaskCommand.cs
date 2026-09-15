using MediatR;

namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand
{
	public sealed record UpdateTaskCommand(Guid TaskId, Guid ProjectId, string Title, string Description, bool Completed) : IRequest;
}
