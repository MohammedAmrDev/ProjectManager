using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Project;
using ProjectManager.Domain.User;

namespace ProjectManager.Application.Features.Projects.Command.DeleteProjectCommand
{
	public class DeleteProjectCommandHandler(ICurrentUserService currentUserService, IProjectRepository projectRepository, IUnitOfWork uow) : IRequestHandler<DeleteProjectCommand, Result>
	{
		public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
		{
			var project = await projectRepository.GetByIdAsync(request.Id);

			if (project == null)
				return ProjectErrors.ProjectNotFound;

			if (!currentUserService.IsAdmin || project.CreatedBy.ToString() != currentUserService.UserId)
				return UserErrors.AccessDenied;

			projectRepository.Delete(project);
			await uow.SaveChangesAsync(cancellationToken);
			return new();
		}
	}
}
