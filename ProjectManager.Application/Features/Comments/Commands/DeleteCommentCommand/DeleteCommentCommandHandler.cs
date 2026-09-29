using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Comment;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Comments.Commands.DeleteCommentCommand
{
	public class DeleteCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWork uow) : IRequestHandler<DeleteCommentCommand, Result>
	{
		public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
			var comment = await commentRepository.GetByIdAsync(request.Id);
			if (comment == null)
				return CommentErrors.CommentNotFound;

			commentRepository.Delete(comment);
			await uow.SaveChangesAsync(cancellationToken);

			return new Result();
		}
	}
}
