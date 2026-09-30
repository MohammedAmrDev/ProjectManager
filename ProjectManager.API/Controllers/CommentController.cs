using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.Extensions;
using ProjectManager.Application.Features.Comments.Commands.CreateCommentCommand;
using ProjectManager.Application.Features.Comments.Commands.DeleteCommentCommand;
using ProjectManager.Application.Features.Comments.Queries.GetCommentsQuery;

namespace ProjectManager.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CommentController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CommentController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{taskId}")]
		public async Task<IActionResult> Get(Guid taskId)
		{
			var taskComments = await _mediator.Send(new GetCommentsQuery(taskId));
			return Ok(taskComments);
		}

		[HttpPost("{taskId}")]
		public async Task<IActionResult> Create(Guid taskId, [FromBody] string content)
		{
			var commentIdResult = await _mediator.Send(new CreateCommentCommand(taskId, content));
			return commentIdResult.IsSuccess ? Ok(commentIdResult.Value) : commentIdResult.ToProblemDetailsResult();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleteResult = await _mediator.Send(new DeleteCommentCommand(id));
			return deleteResult.IsSuccess ? Ok("Delete successed") : deleteResult.ToProblemDetailsResult();
		}
	}
}
