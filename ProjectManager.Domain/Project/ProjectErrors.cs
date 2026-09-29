using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Domain.Project
{
	public static class ProjectErrors
	{
		public static Error ProjectNotFound = new("Project.ProjectNotFound", "Project not found", ErrorType.NotFound);
	}
}
