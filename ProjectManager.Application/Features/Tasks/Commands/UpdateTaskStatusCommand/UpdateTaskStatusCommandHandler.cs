using MediatR;
using ProjectManager.Application.Features.Tasks.Common.Helpers;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Task;


namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskStatusCommand
{
	public class UpdateTaskStatusCommandHandler(ITaskRepository taskRepository, IUnitOfWork uow) : IRequestHandler<UpdateTaskStatusCommand, Result>
	{
		public async Task<Result> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
		{
			var task = await taskRepository.GetByIdAsync(request.Id);
			if (task == null)
				return ProjectTaskErrors.TaskNotFound;

			var checkTaskStatus = task.CheckTaskStatusFlow(request.TaskStatus);
			if (!checkTaskStatus)
				return ProjectTaskErrors.TaskStatusFlowConlict(task.TaskStatus, request.TaskStatus);

			task.TaskStatus = request.TaskStatus;
			taskRepository.Update(task);
			await uow.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}
