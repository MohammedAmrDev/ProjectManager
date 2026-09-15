using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Features.Projects.Queries.GetProjectsQuery
{
	public sealed record GetProjectsQuery : IRequest<List<ProjectResponse>>;
}
