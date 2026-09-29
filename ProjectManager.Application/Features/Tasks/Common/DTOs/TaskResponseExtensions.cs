using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Features.Tasks.Common.DTOs
{
	internal static class TaskResponseExtensions
	{
		internal static TaskResponse ToResponse(this ProjectTask task)
		{
			return new TaskResponse(task.Project.Name, task.Title, task.Description, task.TaskStatus);
		}
	}
}
