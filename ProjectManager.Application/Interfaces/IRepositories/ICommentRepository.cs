using ProjectManager.Domain.Comment;

namespace ProjectManager.Application.Interfaces.IRepositories
{
	public interface ICommentRepository : IGenericRepository<Comment>
	{
		Task<List<Comment>> GetTaskCommentsAsync(Guid taskId);
	}
}
