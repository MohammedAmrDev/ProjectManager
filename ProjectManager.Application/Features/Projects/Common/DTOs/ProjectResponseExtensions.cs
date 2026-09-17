using ProjectManager.Domain.Project;

namespace ProjectManager.Application.Features.Projects.Common.DTOs
{
	public static class ProjectResponseExtensions
	{
		public static ProjectResponse ToResponse(this Project project) =>
			new ProjectResponse(project.Name, project.CreatedAt);
	}
}
