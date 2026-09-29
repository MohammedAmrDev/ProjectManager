using ProjectManager.Domain.Common;
using System.Linq.Expressions;

namespace ProjectManager.Application.Interfaces.IRepositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
		Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
