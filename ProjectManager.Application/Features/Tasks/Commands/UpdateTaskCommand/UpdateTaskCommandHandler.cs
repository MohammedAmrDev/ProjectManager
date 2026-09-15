using MediatR;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand
{
	public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
	{
		private readonly ITaskRepository _taskRepository;
		private readonly IProjectRepository _projectRepository;
		private readonly IUnitOfWork _uow;

		public UpdateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, IUnitOfWork uow)
		{
			_taskRepository = taskRepository;
			_projectRepository = projectRepository;
			_uow = uow;
		}

		public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
		{
			var task = await _taskRepository.GetByIdAsync(request.TaskId);
			if (task is null)
				throw new KeyNotFoundException($"Task with id {request.TaskId} is not found");

			var project = await _projectRepository.GetByIdAsync(request.ProjectId);

			if (project is null)
				throw new KeyNotFoundException($"Project with id {request.ProjectId} is not found");

			task.ProjectId = request.ProjectId;
			task.Title = request.Title;
			task.Description = request.Description;
			task.Completed = request.Completed;

			_taskRepository.Update(task);
			await _uow.SaveChangesAsync(cancellationToken);
		}
	}
}
