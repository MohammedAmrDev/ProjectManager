using MediatR;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Projects.Command.DeleteProjectCommand
{
	public sealed record DeleteProjectCommand(Guid Id) : IRequest<Result>;
}
