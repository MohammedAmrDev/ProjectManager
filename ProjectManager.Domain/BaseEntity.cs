namespace ProjectManager.Domain
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; } = default!;
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdateAt { get; set; }
	}
}