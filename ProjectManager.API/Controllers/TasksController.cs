using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.Requests.Tasks;
using ProjectManager.Application.Features.Tasks.Commands.CreateTaskCommand;
using ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand;
using ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand;
using ProjectManager.Application.Features.Tasks.Queries.GetTaskByIdQuery;
using ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery;

namespace ProjectManager.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TasksController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TasksController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetTasksQuery());
			return Ok(result);
		}

		[HttpGet("{taskId}")]
		public async Task<IActionResult> GetById(Guid taskId)
		{
			var result = await _mediator.Send(new GetTaskByIdQuery(taskId));
			return result is null ? NotFound("Task is not found") : Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateTaskRequest createTaskRequest)
		{
			var taskId = await _mediator.Send(new CreateTaskCommand(createTaskRequest.ProjectId, createTaskRequest.Title, createTaskRequest.Description));
			return CreatedAtRoute("GetById", new { taskId }, null);
		}


		[HttpPut("{taskId}")]
		public async Task<IActionResult> Update(Guid taskId, UpdateTaskRequest updateTaskRequest)
		{
			var command = new UpdateTaskCommand(updateTaskRequest.ProjectId, updateTaskRequest.Title, updateTaskRequest.Description, updateTaskRequest.Completed);
			await _mediator.Send(command);
			return CreatedAtRoute("GetById", new { taskId }, null);
		}

		[HttpDelete("{taskId}")]
		public async Task<IActionResult> Delete(Guid taskId)
		{
			await _mediator.Send(new DeleteTaskCommand(taskId));
			return NoContent();
		}

	}
}
