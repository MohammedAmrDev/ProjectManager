using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class TaskRepository : GenericRepository<Domain.Task.ProjectTask>, ITaskRepository
	{
		public TaskRepository(ApplicationDbContext context) : base(context) { }
	}
}
