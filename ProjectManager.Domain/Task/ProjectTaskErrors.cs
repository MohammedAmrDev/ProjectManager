using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Domain.Task
{
	public static class ProjectTaskErrors
	{
		public static Error TaskNotFound = new("Task.TaskNotFound", "Task not found", ErrorType.NotFound);
		public static Error TaskStatusFlowConlict(ProjectTaskStatus taskStatus, ProjectTaskStatus requestStatus) => new("Task.TaskStatusFlowConlict", $"Can not update task status from {taskStatus.ToString().ToLower()} to {requestStatus.ToString().ToLower()}", ErrorType.Conflict);
	}
}
