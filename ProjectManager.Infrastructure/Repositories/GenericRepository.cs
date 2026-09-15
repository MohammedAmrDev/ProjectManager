using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain;
using ProjectManager.Infrastructure.Data;
using System.Linq.Expressions;

namespace ProjectManager.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext context) =>
			_dbSet = context.Set<T>();

		public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return query.ToListAsync();
		}

		public Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return query.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<Guid> AddAsync(T entity)
		{
			var addedEntity = await _dbSet.AddAsync(entity);
			return addedEntity.Entity.Id;
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}
	}
}
