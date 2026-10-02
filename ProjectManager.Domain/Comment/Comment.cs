using ProjectManager.Domain.Common;

namespace ProjectManager.Domain.Comment
{
	public class Comment : BaseEntity
	{
		public string Content { get; set; } = string.Empty;
		public Guid TaskId { get; set; }
	}
}