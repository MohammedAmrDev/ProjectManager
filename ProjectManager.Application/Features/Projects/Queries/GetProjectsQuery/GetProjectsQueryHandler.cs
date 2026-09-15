using MediatR;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Projects.Queries.GetProjectsQuery
{
	internal class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectResponse>>
	{
		private readonly IProjectRepository _projectRepository;

		public GetProjectsQueryHandler(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		public async Task<List<ProjectResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
		{
			var projects = await _projectRepository.GetAllAsync();
			return projects.Select(p => new ProjectResponse(p.Name, p.CreatedAt)).ToList();
		}
	}
}
