using FluentValidation;

namespace ProjectManager.Application.Features.Projects.Command.DeleteProjectCommand
{
	public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
	{
		public DeleteProjectCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("Task id is required");
		}
	}
}
