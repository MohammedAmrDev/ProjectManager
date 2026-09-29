using FluentValidation;

namespace ProjectManager.Application.Features.Comments.Commands.CreateCommentCommand
{
	public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
	{
		public CreateCommentCommandValidator()
		{
			RuleFor(c => c.Content)
				.MaximumLength(200).WithMessage("Maximum length is 200");
		}
	}
}
