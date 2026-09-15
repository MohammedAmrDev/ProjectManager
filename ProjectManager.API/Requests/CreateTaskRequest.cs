namespace ProjectManager.API.Requests
{
	public class CreateTaskRequest
	{
		public Guid ProjectId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}