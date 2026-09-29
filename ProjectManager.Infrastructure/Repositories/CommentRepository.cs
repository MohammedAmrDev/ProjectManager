using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Comment;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{
		private readonly ApplicationDbContext _context;
		public CommentRepository(ApplicationDbContext context) : base(context) =>
			_context = context;

		public async Task<List<Comment>> GetTaskCommentsAsync(Guid taskId) =>
			await _context.Comments.Where(c => c.TaskId == taskId).ToListAsync();
	}
}
