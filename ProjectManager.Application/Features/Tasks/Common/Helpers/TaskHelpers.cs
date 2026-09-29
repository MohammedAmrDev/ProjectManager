using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Features.Tasks.Common.Helpers
{
	public static class TaskHelpers
	{
		public static bool CheckTaskStatusFlow(this ProjectTask task, ProjectTaskStatus requestStatus)
		{
			Dictionary<ProjectTaskStatus, List<ProjectTaskStatus>> AllowedTransitions = new()
			{
				[ProjectTaskStatus.Todo] = [ProjectTaskStatus.InProgress, ProjectTaskStatus.Cancelled],
				[ProjectTaskStatus.InProgress] = [ProjectTaskStatus.Completed, ProjectTaskStatus.Cancelled],
				[ProjectTaskStatus.Completed] = [],
				[ProjectTaskStatus.Cancelled] = []
			};

			return AllowedTransitions.TryGetValue(task.TaskStatus, out var taskStatuses) && taskStatuses.Contains(requestStatus);
		}
	}
}
