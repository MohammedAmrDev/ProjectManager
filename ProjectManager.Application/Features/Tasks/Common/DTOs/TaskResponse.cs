using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Features.Tasks.Common.DTOs
{
	public sealed record TaskResponse(string ProjectName, string Title, string Description, ProjectTaskStatus TaskStatus);
}