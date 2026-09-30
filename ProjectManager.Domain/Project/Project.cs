using ProjectManager.Domain.Common;

namespace ProjectManager.Domain.Project
{
	public class Project : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		//public Guid OwnerId { get; set; }
	}
}
