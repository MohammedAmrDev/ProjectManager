using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery
{
	public sealed record GetTasksQuery() : IRequest<List<TaskResponse>>;
}
