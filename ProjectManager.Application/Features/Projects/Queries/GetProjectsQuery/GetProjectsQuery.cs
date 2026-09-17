using MediatR;
using ProjectManager.Application.Features.Projects.Common.DTOs;

namespace ProjectManager.Application.Features.Projects.Queries.GetProjectsQuery
{
	public sealed record GetProjectsQuery : IRequest<List<ProjectResponse>>;
}
