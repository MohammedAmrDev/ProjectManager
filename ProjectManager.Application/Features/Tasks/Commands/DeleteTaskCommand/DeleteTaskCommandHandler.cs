using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Task;
using ProjectManager.Domain.User;

namespace ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand
{
	public class DeleteTaskCommandHandler(ICurrentUserService currentUserService, ITaskRepository taskRepository, IUnitOfWork uow) : IRequestHandler<DeleteTaskCommand, Result>
	{

		public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
		{
			var task = await taskRepository.GetByIdAsync(request.TaskId);
			if (task is null)
				return ProjectTaskErrors.TaskNotFound;

			if (!currentUserService.IsAdmin || task.CreatedBy.ToString() != currentUserService.UserId)
				return UserErrors.AccessDenied;

			taskRepository.Delete(task);
			await uow.SaveChangesAsync(cancellationToken);
			return new();
		}
	}
}
