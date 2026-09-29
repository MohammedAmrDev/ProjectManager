using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand
{
	public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
	{
		private readonly ITaskRepository _taskRepository;
		private readonly IUnitOfWork _uow;

		public DeleteTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork uow)
		{
			_taskRepository = taskRepository;
			_uow = uow;
		}

		public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
		{
			var task = await _taskRepository.GetByIdAsync(request.TaskId);
			if (task is null)
				return ProjectTaskErrors.TaskNotFound;
			_taskRepository.Delete(task);
			await _uow.SaveChangesAsync(cancellationToken);
			return new();
		}
	}
}
