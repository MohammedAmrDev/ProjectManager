using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.Requests.Projects;
using ProjectManager.API.Requests.Tasks;
using ProjectManager.Application.Features.Projects.Command.CreateProjectCommand;
using ProjectManager.Application.Features.Projects.Queries.GetProjectsQuery;
using ProjectManager.Application.Features.Tasks.Commands.CreateTaskCommand;
using ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand;
using ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery;

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
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetProjectsQuery());
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectRequest createProjectRequest)
		{
			var taskId = await _mediator.Send(new CreateProjectCommand(createProjectRequest.Name));
			return CreatedAtRoute("Get", new {  }, null);
		}
	}
}
