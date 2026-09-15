using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTaskByIdQuery
{
	public sealed record GetTaskByIdQuery(Guid TaskId) : IRequest<TaskResponse?>;
}
