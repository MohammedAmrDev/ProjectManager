using MediatR;
using ProjectManager.Application.Features.Projects.Common.DTOs;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Project;

namespace ProjectManager.Application.Features.Projects.Command.CreateProjectCommand
{
	public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResponse>
	{
		private readonly IProjectRepository _projectRepository;
		private readonly IUnitOfWork _uow;

		public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork uow)
		{
			_projectRepository = projectRepository;
			_uow = uow;
		}

		public async Task<ProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
		{
			var project = new Project
			{
				Name = request.Name,
			};

			_projectRepository.Add(project);
			await _uow.SaveChangesAsync(cancellationToken);
			return project.ToResponse();
		}
	}
}
