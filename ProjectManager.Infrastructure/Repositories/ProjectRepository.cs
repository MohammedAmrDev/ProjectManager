using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Project;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class ProjectRepository : GenericRepository<Project>, IProjectRepository
	{
		public ProjectRepository(ApplicationDbContext context) : base(context) {  }
	}
}
