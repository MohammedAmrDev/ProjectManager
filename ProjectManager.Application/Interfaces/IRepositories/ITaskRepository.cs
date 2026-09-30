using ProjectManager.Domain.Task;

namespace ProjectManager.Application.Interfaces.IRepositories
{
	public interface ITaskRepository : IGenericRepository<ProjectTask>
	{
		Task<List<ProjectTask>> GetProjectTasksAsync(Guid projectId);
	}
}
