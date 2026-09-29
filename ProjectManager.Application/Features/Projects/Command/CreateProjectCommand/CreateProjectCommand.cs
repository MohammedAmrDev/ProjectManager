using MediatR;
using ProjectManager.Application.Features.Projects.Common.DTOs;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Features.Projects.Command.CreateProjectCommand
{
	public sealed record CreateProjectCommand(string Name) : IRequest<ProjectResponse>;
}
