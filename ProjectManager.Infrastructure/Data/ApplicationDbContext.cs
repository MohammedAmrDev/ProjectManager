using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Comment;
using ProjectManager.Domain.Project;
using ProjectManager.Domain.Task;
using ProjectManager.Domain.User;
using ProjectManager.Infrastructure.Identity.Entities;

namespace ProjectManager.Infrastructure.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
	{
		public DbSet<Project> Projects { get; set; }
		public DbSet<ProjectTask> Tasks { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
	}
}
