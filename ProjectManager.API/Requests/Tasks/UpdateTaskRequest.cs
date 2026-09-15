namespace ProjectManager.API.Requests.Tasks
{
	public class UpdateTaskRequest
	{
		public Guid ProjectId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool Completed { get; set; }
	}
}