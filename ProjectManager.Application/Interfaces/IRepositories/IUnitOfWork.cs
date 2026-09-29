using System.Data;

namespace ProjectManager.Application.Interfaces.IRepositories
{
	public interface IUnitOfWork
	{
		IDbTransaction BeginTransaction();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
