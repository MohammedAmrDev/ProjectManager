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
			var project = await _projectRepository.GetByIdAsync(request.ProjectId);
			if (project is null)
				throw new KeyNotFoundException($"Project with id {request.ProjectId} is not found");

			var task = new Domain.Task.Task
			{
				ProjectId = request.ProjectId,
				Title = request.Title,
				Description = request.Description,
				Completed = request.Completed,
			};
			_taskRepository.Update(task);
			await _uow.SaveChangesAsync(cancellationToken);
		}
	}
}
