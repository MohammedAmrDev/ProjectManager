using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery
{
	public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskResponse>>
	{
		private readonly ITaskRepository _taskRepository;

		public GetTasksQueryHandler(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<List<TaskResponse>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
		{
			var taskResponses = await _taskRepository.GetAllAsync(x => x.Project);
			return taskResponses.Select(t => new TaskResponse(t.Project.Name, t.Title, t.Description, t.Completed)).ToList();
		}
	}
}
