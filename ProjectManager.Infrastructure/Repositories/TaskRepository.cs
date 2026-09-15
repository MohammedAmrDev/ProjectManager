using ProjectManager.Application.Interfaces;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class TaskRepository : GenericRepository<Domain.Task.Task>, ITaskRepository
	{
		public TaskRepository(ApplicationDbContext context) : base(context) { }
	}
}
