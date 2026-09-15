using MediatR;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Project;

namespace ProjectManager.Application.Features.Projects.Command.CreateProjectCommand
{
	public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
	{
		private readonly IProjectRepository _projectRepository;
		private readonly IUnitOfWork _uow;

		public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork uow)
		{
			_projectRepository = projectRepository;
			_uow = uow;
		}

		public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
		{
			var project = new Project
			{
				Name = request.Name,
			};

			var projectId = await _projectRepository.AddAsync(project);
			await _uow.SaveChangesAsync(cancellationToken);
			return projectId;
		}
	}
}
