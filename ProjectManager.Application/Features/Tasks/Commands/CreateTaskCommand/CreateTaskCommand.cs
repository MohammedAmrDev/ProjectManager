using MediatR;

namespace ProjectManager.Application.Features.Tasks.Commands.CreateTaskCommand
{
	public sealed record CreateTaskCommand(Guid ProjectId, string Title, string Description) : IRequest<Guid>;
}
