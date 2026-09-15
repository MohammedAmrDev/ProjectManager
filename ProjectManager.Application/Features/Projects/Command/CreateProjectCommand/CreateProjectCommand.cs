using MediatR;

namespace ProjectManager.Application.Features.Projects.Command.CreateProjectCommand
{
	public sealed record CreateProjectCommand(string Name) : IRequest<Guid>;
}
