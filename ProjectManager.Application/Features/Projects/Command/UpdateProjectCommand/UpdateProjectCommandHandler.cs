using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Project;
using ProjectManager.Domain.User;

namespace ProjectManager.Application.Features.Projects.Command.UpdateProjectCommand
{
	public class UpdateProjectCommandHandler(ICurrentUserService currentUserService, IProjectRepository projectRepository, IUnitOfWork uow) : IRequestHandler<UpdateProjectCommand, Result>
	{
		public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
		{
			var project = await projectRepository.GetByIdAsync(request.Id);

			if (project == null)
				return ProjectErrors.ProjectNotFound;

			if (!currentUserService.IsAdmin || project.CreatedBy.ToString() != currentUserService.UserId)
				return UserErrors.AccessDenied;

			project.Name = request.Name;

			projectRepository.Update(project);
			await uow.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}
