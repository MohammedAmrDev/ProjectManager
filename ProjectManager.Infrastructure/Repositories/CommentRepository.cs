using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Comment;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{
		public CommentRepository(ApplicationDbContext context) : base(context) {  }
	}
}
