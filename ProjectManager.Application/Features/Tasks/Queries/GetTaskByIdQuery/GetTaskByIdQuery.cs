using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTaskByIdQuery
{
	public sealed record GetTaskByIdQuery(Guid TaskId) : IRequest<Result<TaskResponse>>;
}
