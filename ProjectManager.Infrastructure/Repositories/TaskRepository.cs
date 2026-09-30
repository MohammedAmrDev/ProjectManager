using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Task;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class TaskRepository(ApplicationDbContext context) : GenericRepository<ProjectTask>(context), ITaskRepository
	{
		public async Task<List<ProjectTask>> GetProjectTasksAsync(Guid projectId) =>
			await context.Tasks.Where(t => t.ProjectId == projectId).Include(t => t.Project).ToListAsync();
	}
}
