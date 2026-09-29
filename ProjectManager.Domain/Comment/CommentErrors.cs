using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Domain.Comment
{
	public static class CommentErrors
	{
		public static Error CommentNotFound = new("Comment.CommentNotFound", "Comment not found", ErrorType.NotFound);
	}
}
