using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain;

namespace ProjectManager.Infrastructure.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<Domain.Project.Project> Projects { get; set; }
		public DbSet<Domain.Task.Task> Tasks { get; set; }
		public DbSet<Domain.Comment.Comment> Comments { get; set; }

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			var entries = ChangeTracker.Entries<BaseEntity>();

			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedAt = DateTime.UtcNow;
					entry.Entity.UpdateAt = DateTime.UtcNow;
				}
				else if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdateAt = DateTime.UtcNow;
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
