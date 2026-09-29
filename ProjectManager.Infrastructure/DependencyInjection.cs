using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Application.Interfaces.IServices;
using ProjectManager.Infrastructure.Data;
using ProjectManager.Infrastructure.Identity.Services;
using ProjectManager.Infrastructure.Repositories;

namespace ProjectManager.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
		{
			// Registering repositories and uow
			services.AddScoped<IProjectRepository, ProjectRepository>();
			services.AddScoped<ITaskRepository, TaskRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Registering DbContext
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection")));

			// Registering Auth Service
			services.AddScoped<IAuthService, AuthService>();

			return services;
		}
	}
}
