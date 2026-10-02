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
				ErrorType.BadRequest => StatusCodes.Status400BadRequest,
				ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
				ErrorType.Forbidden => StatusCodes.Status403Forbidden,
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.Validation => StatusCodes.Status400BadRequest,
				_ => StatusCodes.Status500InternalServerError,
			};

			var problemTitle = result.Error.ErrorType switch
			{
				ErrorType.BadRequest => "Bad Request",
				ErrorType.Unauthorized => "Unathorized Access",
				ErrorType.Forbidden => "Access Denied",
				ErrorType.NotFound => "Resourse Not Found",
				ErrorType.Conflict => "Conflict Occurred",
				ErrorType.Validation => "Validation Error",
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
