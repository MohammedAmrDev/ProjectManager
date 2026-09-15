using FluentValidation;
using ProjectManager.Application.Interfaces;

namespace ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand
{
	public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
	{
		public UpdateTaskCommandValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title is required")
				.MaximumLength(40).WithMessage("Maximum length for title is 40");
			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Description is required")
				.MaximumLength(200).WithMessage("Maximum length for description is 200");
		}
	}
}
