using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common;
using ProjectManager.Infrastructure.Data;
using System.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext context) =>
			_context = context;

		public IDbTransaction BeginTransaction() =>
			_context.Database.BeginTransaction().GetDbTransaction();

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = _context.ChangeTracker.Entries<BaseEntity>();

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
			return await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
