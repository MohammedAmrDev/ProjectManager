using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTaskByIdQuery
{
	internal class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskResponse?>
	{
		private readonly ITaskRepository _taskRepository;

		public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<TaskResponse?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
		{
			var task = await _taskRepository.GetByIdAsync(request.TaskId, x => x.Project);

			return task is null ? null : new TaskResponse(task.Project.Name, task.Title, task.Description, task.Completed);
		}
	}
}
