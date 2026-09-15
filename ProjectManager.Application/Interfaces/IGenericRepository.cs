using ProjectManager.Domain;
using System.Linq.Expressions;

namespace ProjectManager.Application.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
		Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
		Task<Guid> AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
