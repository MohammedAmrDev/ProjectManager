using MediatR;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Tasks.Commands.CreateTaskCommand
{
	public class UpdateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
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

		public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
		{
			var project = await _projectRepository.GetByIdAsync(request.ProjectId);
			if (project is null)
				throw new KeyNotFoundException($"Project with id {request.ProjectId} is not found");

			var task = new Domain.Task.Task
			{
				ProjectId = request.ProjectId,
				Title = request.Title,
				Description = request.Description,
			};

			_taskRepository.Add(task);
			await _uow.SaveChangesAsync(cancellationToken);
			return task.Id;
		}
	}
}
