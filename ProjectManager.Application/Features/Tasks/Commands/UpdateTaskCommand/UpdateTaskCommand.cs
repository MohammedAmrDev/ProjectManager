using MediatR;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand
{
	public sealed record UpdateTaskCommand(Guid TaskId, Guid ProjectId, string Title, string Description, ProjectTaskStatus TaskStatus) : IRequest<Result>;
}
