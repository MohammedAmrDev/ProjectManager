using FluentValidation;

namespace ProjectManager.Application.Features.Projects.Command.UpdateProjectCommand
{
	public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
	{
		public UpdateProjectCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("Task id is required");
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Project name is required")
				.MaximumLength(40).WithMessage("Maximum length for project name is 40");
		}
	}
}
