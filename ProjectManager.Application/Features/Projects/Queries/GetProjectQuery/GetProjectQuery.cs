using MediatR;
using ProjectManager.Application.Features.Projects.Common.DTOs;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Projects.Queries.GetProjectQuery
{
	public sealed record GetProjectQuery(Guid Id) : IRequest<Result<ProjectResponse>>;
}
