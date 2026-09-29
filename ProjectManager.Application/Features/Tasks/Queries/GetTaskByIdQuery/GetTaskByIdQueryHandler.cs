using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTaskByIdQuery
{
	internal class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskResponse>>
	{
		private readonly ITaskRepository _taskRepository;

		public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<Result<TaskResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
		{
			var task = await _taskRepository.GetByIdAsync(request.TaskId, x => x.Project);
			if (task is null)
				return ProjectTaskErrors.TaskNotFound;

			return task.ToResponse();
		}
	}
}
