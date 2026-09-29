using Microsoft.AspNetCore.Mvc;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.API.Extensions
{
	public static class ResultExtensions
	{
		public static IActionResult ToProblemDetailsResult(this Result result)
		{
			if (result.IsSuccess)
				throw new InvalidOperationException("Cann't convert a sucess result to a problem details response");

			var statusCode = result.Error.ErrorType switch
			{
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				ErrorType.Validation => StatusCodes.Status400BadRequest,
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.BadRequest => StatusCodes.Status400BadRequest,
				ErrorType.Forbidden => StatusCodes.Status403Forbidden,
				ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
				_ => StatusCodes.Status500InternalServerError,
			};

			var problemTitle = result.Error.ErrorType switch
			{
				ErrorType.NotFound => "Resourse Not Found",
				ErrorType.Validation => "Validation Error",
				ErrorType.Conflict => "Conflict Occurred",
				ErrorType.BadRequest => "Bad Request",
				ErrorType.Forbidden => "Access Denied",
				ErrorType.Unauthorized => "Unathorized Access",
				_ => "Internal Server Error Occurred",
			};

			var problemDetails = new ProblemDetails
			{
				Status = statusCode,
				Title = problemTitle,
				Detail = result.Error.Description,
			};

			return new ObjectResult(problemDetails)
			{
				StatusCode = statusCode,
			};
		}
	}
}
