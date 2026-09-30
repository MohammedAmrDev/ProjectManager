using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.Extensions;
using ProjectManager.API.Requests.Projects;
using ProjectManager.Application.Features.Projects.Command.CreateProjectCommand;
using ProjectManager.Application.Features.Projects.Command.DeleteProjectCommand;
using ProjectManager.Application.Features.Projects.Command.UpdateProjectCommand;
using ProjectManager.Application.Features.Projects.Queries.GetProjectQuery;
using ProjectManager.Application.Features.Projects.Queries.GetProjectsQuery;

namespace ProjectManager.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProjectController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProjectController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Get()
		{
			var projectResponses = await _mediator.Send(new GetProjectsQuery());
			return Ok(projectResponses);
		}

		[HttpGet("{projectId}")]
		public async Task<IActionResult> GetById(Guid projectId)
		{
			var projectResult = await _mediator.Send(new GetProjectQuery(projectId));
			return projectResult.IsSuccess ? Ok(projectResult.Value) : projectResult.ToProblemDetailsResult();
		}

		[HttpPost]
		//[Authorize]
		public async Task<IActionResult> Create(CreateProjectRequest createProjectRequest)
		{
			var projectId = await _mediator.Send(new CreateProjectCommand(createProjectRequest.Name));
			return Ok(projectId);
		}

		[HttpPut("{projectId}")]
		//[Authorize] // project owner can only update
		public async Task<IActionResult> Update(Guid projectId, string projectName)
		{
			var updateResult = await _mediator.Send(new UpdateProjectCommand(projectId, projectName));
			return updateResult.IsSuccess ? Ok("Project updated successfully") : updateResult.ToProblemDetailsResult();
		}

		[HttpDelete("{projectId}")]
		public async Task<IActionResult> Delete(Guid projectId)
		{
			var deleteResult = await _mediator.Send(new DeleteProjectCommand(projectId));
			return deleteResult.IsSuccess ? Ok("Project deleted successfully") : deleteResult.ToProblemDetailsResult();
		}
	}
}
