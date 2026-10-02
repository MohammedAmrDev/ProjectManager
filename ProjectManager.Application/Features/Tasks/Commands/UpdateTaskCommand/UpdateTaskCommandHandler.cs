using MediatR;
using ProjectManager.Application.Features.Tasks.Common.Helpers;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Project;
using ProjectManager.Domain.Task;
using ProjectManager.Domain.User;

namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand
{
	public class UpdateTaskCommandHandler(ICurrentUserService currentUserService, ITaskRepository taskRepository, IProjectRepository projectRepository, IUnitOfWork uow) : IRequestHandler<UpdateTaskCommand, Result>
	{
		public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
		{
			var task = await taskRepository.GetByIdAsync(request.TaskId);
			if (task is null)
				return ProjectTaskErrors.TaskNotFound;

			if (!currentUserService.IsAdmin || task.CreatedBy.ToString() != currentUserService.UserId)
				return UserErrors.AccessDenied;

			var project = await projectRepository.GetByIdAsync(request.ProjectId);
			if (project is null)
				return ProjectErrors.ProjectNotFound;

			var checkTaskStatus = task.CheckTaskStatusFlow(request.TaskStatus);
			if (!checkTaskStatus)
				return ProjectTaskErrors.TaskStatusFlowConlict(task.TaskStatus, request.TaskStatus);


			task.ProjectId = request.ProjectId;
			task.Title = request.Title;
			task.Description = request.Description;
			task.TaskStatus = request.TaskStatus;

			taskRepository.Update(task);
			
			await uow.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}
