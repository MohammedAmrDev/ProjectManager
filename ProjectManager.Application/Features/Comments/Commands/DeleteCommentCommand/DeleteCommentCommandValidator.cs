using FluentValidation;

namespace ProjectManager.Application.Features.Comments.Commands.DeleteCommentCommand
{
	public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
	{
		public DeleteCommentCommandValidator()
		{

			RuleFor(c => c.Id)
				.NotEmpty().WithMessage("Comment id is required");
		}
	}
}
