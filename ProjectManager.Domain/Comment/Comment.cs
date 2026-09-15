namespace ProjectManager.Domain.Comment
{
	public class Comment : BaseEntity
	{
		public string CommentText { get; set; } = string.Empty;
		public Guid TaskId { get; set; }
	}
}
