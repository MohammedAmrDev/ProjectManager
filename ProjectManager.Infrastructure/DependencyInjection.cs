using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Interfaces;
using ProjectManager.Infrastructure.Repositories;

namespace ProjectManager.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddScoped<IProjectRepository, ProjectRepository>();
			services.AddScoped<ITaskRepository, TaskRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
