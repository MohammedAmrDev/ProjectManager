using FluentValidation;

namespace ProjectManager.Application.Features.Projects.Command.CreateProjectCommand
{
	public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
	{
		public CreateProjectCommandValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Title is required")
				.MaximumLength(40).WithMessage("Maximum length for title is 40");
		}
	}
}
