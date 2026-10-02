namespace ProjectManager.Domain.Common
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; } = Guid.CreateVersion7();
		public Guid CreatedBy { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdateAt { get; set; }
	}
}