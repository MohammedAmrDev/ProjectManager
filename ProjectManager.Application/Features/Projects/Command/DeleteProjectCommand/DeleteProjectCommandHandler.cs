using MediatR;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.Project;

namespace ProjectManager.Application.Features.Projects.Command.DeleteProjectCommand
{
	public class DeleteProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork uow) : IRequestHandler<DeleteProjectCommand, Result>
	{
		public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
		{
			var project = await projectRepository.GetByIdAsync(request.Id);

			if (project == null)
				return ProjectErrors.ProjectNotFound;

			projectRepository.Delete(project);
			await uow.SaveChangesAsync(cancellationToken);
			return new();
		}
	}
}
