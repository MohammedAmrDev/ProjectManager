namespace ProjectManager.Application.Features.Tasks.Common.DTOs
{
	internal static class TaskResponseExtensions
	{
		internal static TaskResponse ToResponse(this Domain.Task.Task task)
		{
			return new TaskResponse(task.Project.Name, task.Title, task.Description, task.Completed);
		}
	}
}
