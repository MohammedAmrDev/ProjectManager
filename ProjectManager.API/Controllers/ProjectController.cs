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
	[Authorize]
	public class ProjectController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProjectController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetProjectsQuery());
			return Ok(result);
		}

		[HttpGet("{projectId}")]
		public async Task<IActionResult> GetById(Guid projectId)
		{
			var result = await _mediator.Send(new GetProjectQuery(projectId));
			return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetailsResult();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectRequest createProjectRequest)
		{
			var projectId = await _mediator.Send(new CreateProjectCommand(createProjectRequest.Name));
			return Ok(projectId);
		}

		[HttpPut("{projectId}")]
		public async Task<IActionResult> Update(Guid projectId, string projectName)
		{
			var result = await _mediator.Send(new UpdateProjectCommand(projectId, projectName));
			return result.IsSuccess ? Ok("Project updated successfully") : result.ToProblemDetailsResult();
		}

		[HttpDelete("{projectId}")]
		public async Task<IActionResult> Delete(Guid projectId)
		{
			var result = await _mediator.Send(new DeleteProjectCommand(projectId));
			return result.IsSuccess ? Ok("Project deleted successfully") : result.ToProblemDetailsResult();
		}
	}
}
