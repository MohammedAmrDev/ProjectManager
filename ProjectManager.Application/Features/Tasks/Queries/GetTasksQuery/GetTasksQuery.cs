using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery
{
	public sealed record GetTasksQuery(Guid ProjectId) : IRequest<Result<List<TaskResponse>>>;
}
