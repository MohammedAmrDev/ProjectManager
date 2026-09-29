using MediatR;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Projects.Command.UpdateProjectCommand
{
	public sealed record UpdateProjectCommand(Guid Id, string Name) : IRequest<Result>;
}
