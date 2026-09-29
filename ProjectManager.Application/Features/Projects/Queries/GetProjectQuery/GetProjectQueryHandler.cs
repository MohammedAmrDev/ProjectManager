using MediatR;
using ProjectManager.Application.Features.Projects.Common.DTOs;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Project;

namespace ProjectManager.Application.Features.Projects.Queries.GetProjectQuery
{
	public class GetProjectQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectQuery, Result<ProjectResponse>>
	{
		public async Task<Result<ProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
		{
			var project = await projectRepository.GetByIdAsync(request.Id);
			if (project == null)
				return ProjectErrors.ProjectNotFound;

			return project.ToResponse();
		}
	}
}
