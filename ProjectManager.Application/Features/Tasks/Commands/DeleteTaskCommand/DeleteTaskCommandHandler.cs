using MediatR;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand
{
	public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
	{
		private readonly ITaskRepository _taskRepository;
		private readonly IUnitOfWork _uow;

		public DeleteTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork uow)
		{
			_taskRepository = taskRepository;
			_uow = uow;
		}

		public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
		{
			var task = await _taskRepository.GetByIdAsync(request.TaskId);
			if (task is null)
				throw new KeyNotFoundException($"Task with id ${request.TaskId}");
			_taskRepository.Delete(task);
			await _uow.SaveChangesAsync(cancellationToken);
		}
	}
}
