using MediatR;
using ProjectManager.Domain.Common.Result;
using ProjectTaskStatus =  ProjectManager.Domain.Task.ProjectTaskStatus;

namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskStatusCommand
{
	public sealed record UpdateTaskStatusCommand(Guid Id, ProjectTaskStatus TaskStatus) : IRequest<Result>;
}
