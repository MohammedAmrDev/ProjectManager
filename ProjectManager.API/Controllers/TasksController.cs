using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.Extensions;
using ProjectManager.API.Requests.Tasks;
using ProjectManager.Application.Features.Tasks.Commands.CreateTaskCommand;
using ProjectManager.Application.Features.Tasks.Commands.DeleteTaskCommand;
using ProjectManager.Application.Features.Tasks.Commands.UpdateTaskCommand;
using ProjectManager.Application.Features.Tasks.Commands.UpdateTaskStatusCommand;
using ProjectManager.Application.Features.Tasks.Queries.GetTaskByIdQuery;
using ProjectManager.Application.Features.Tasks.Queries.GetTasksQuery;
using ProjectManager.Domain.Task;
using System.Security.Claims;

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
		public async Task<IActionResult> Get(Guid projectId)
		{
			var taskResponsesResult = await _mediator.Send(new GetTasksQuery(projectId));
			return taskResponsesResult.IsSuccess ? Ok(taskResponsesResult.Value) : taskResponsesResult.ToProblemDetailsResult();
		}

		[HttpGet("{taskId}")]
		public async Task<IActionResult> GetById(Guid taskId)
		{
			var taskResponseResult = await _mediator.Send(new GetTaskByIdQuery(taskId));
			return taskResponseResult.IsSuccess ? Ok(taskResponseResult.Value) : taskResponseResult.ToProblemDetailsResult();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(CreateTaskRequest createTaskRequest)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
			var taskIdResult = await _mediator.Send(new CreateTaskCommand(createTaskRequest.ProjectId, createTaskRequest.Title, createTaskRequest.Description, Guid.Parse(userId)));
			return taskIdResult.IsSuccess ? Ok(taskIdResult.Value) : taskIdResult.ToProblemDetailsResult();
		}

		[HttpPut("{taskId}")]
		public async Task<IActionResult> Update(Guid taskId, UpdateTaskRequest updateTaskRequest)
		{
			var command = new UpdateTaskCommand(taskId, updateTaskRequest.ProjectId, updateTaskRequest.Title, updateTaskRequest.Description, updateTaskRequest.TaskStatus);
			var updateResult = await _mediator.Send(command);
			return updateResult.IsSuccess ? Ok("Task updated successfully") : updateResult.ToProblemDetailsResult();
		}

		[HttpPatch("{taskId}")]
		public async Task<IActionResult> UpdateTaskStatus(Guid taskId, ProjectTaskStatus taskStatus)
		{
			var command = new UpdateTaskStatusCommand(taskId, taskStatus);
			var updateResult = await _mediator.Send(command);
			return updateResult.IsSuccess ? Ok("Task updated successfully") : updateResult.ToProblemDetailsResult();
		}

		[HttpDelete("{taskId}")]
		public async Task<IActionResult> Delete(Guid taskId)
		{
			var deleteResult = await _mediator.Send(new DeleteTaskCommand(taskId));
			return deleteResult.IsSuccess ? Ok("Task deleted successfully") : deleteResult.ToProblemDetailsResult();
		}
	}
}
