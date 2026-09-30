using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Comments.Queries.GetCommentsQuery
{
	public class GetCommentsQueryHandler(ICommentRepository commentRepository) : IRequestHandler<GetCommentsQuery, List<CommentResponse>>
	{
		public async Task<List<CommentResponse>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
		{
			var comments = await commentRepository.GetTaskCommentsAsync(request.TaskId);
			return comments.Select(c => new CommentResponse(c.Content, c.TaskId)).ToList();
		}
	}
}
