

namespace ProjectManager.Domain.Task
{
	public class Task : BaseEntity
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Guid ProjectId { get; set; }
		public Project.Project Project { get; set; }
		public bool Completed { get; set; }
	}
}
