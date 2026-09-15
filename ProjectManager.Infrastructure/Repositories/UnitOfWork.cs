using ProjectManager.Application.Interfaces;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext context) =>
			_context = context;

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
			await _context.SaveChangesAsync(cancellationToken);
	}
}
