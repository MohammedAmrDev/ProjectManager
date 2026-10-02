using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Comment;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Comments.Commands.CreateCommentCommand
{
	public class CreateCommentCommandHandler(ICommentRepository commentRepository, ITaskRepository taskRepository, IUnitOfWork uow) : IRequestHandler<CreateCommentCommand, Result<Guid>>
	{
		public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
		{
			var task = await taskRepository.GetByIdAsync(request.TaskId);

			if (task is null)
				return CommentErrors.CommentNotFound;

			var comment = new Comment
			{
				TaskId = request.TaskId,
				Content = request.Content,
				CreatedBy = request.CreateBy
			};

			commentRepository.Add(comment);
			await uow.SaveChangesAsync(cancellationToken);

			return comment.Id;
		}
	}
}
