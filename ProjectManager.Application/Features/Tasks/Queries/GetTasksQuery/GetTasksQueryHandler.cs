using MediatR;
using ProjectManager.Application.Features.Tasks.Common.DTOs;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Project;

namespace ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery
{
	public class GetTasksQueryHandler(ITaskRepository taskRepository, IProjectRepository projectRepository) : IRequestHandler<GetTasksQuery, Result<List<TaskResponse>>>
	{
		public async Task<Result<List<TaskResponse>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
		{
			var project = await projectRepository.GetByIdAsync(request.ProjectId);
			if (project is null)
				return ProjectErrors.ProjectNotFound;


			var taskResponses = await taskRepository.GetProjectTasksAsync(request.ProjectId);
			return taskResponses.Select(t => t.ToResponse()).ToList();
		}
	}
}
