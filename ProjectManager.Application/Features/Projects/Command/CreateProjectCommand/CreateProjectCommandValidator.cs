using FluentValidation;

namespace ProjectManager.Application.Features.Projects.Command.CreateProjectCommand
{
	public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
	{
		public CreateProjectCommandValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Project name is required")
				.MaximumLength(40).WithMessage("Maximum length for project name is 40");
		}
	}
}
