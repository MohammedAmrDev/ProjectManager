namespace ProjectManager.API.Requests.Tasks
{
	public class CreateTaskRequest
	{
		public Guid ProjectId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}