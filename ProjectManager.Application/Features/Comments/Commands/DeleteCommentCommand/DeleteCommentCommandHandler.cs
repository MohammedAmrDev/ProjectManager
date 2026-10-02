using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Comment;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.User;

namespace ProjectManager.Application.Features.Comments.Commands.DeleteCommentCommand
{
	public class DeleteCommentCommandHandler(ICurrentUserService currentUserService, ICommentRepository commentRepository, IUnitOfWork uow) : IRequestHandler<DeleteCommentCommand, Result>
	{
		public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
			var comment = await commentRepository.GetByIdAsync(request.Id);
			if (comment == null)
				return CommentErrors.CommentNotFound;

			if (!currentUserService.IsAdmin || comment.CreatedBy.ToString() != currentUserService.UserId)
				return UserErrors.AccessDenied;

			commentRepository.Delete(comment);
			await uow.SaveChangesAsync(cancellationToken);

			return new Result();
		}
	}
}
