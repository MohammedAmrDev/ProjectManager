using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManager.API.Handlers
{
	public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

			await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Server Error",
				Detail = "An unexpected error occurred on our end. Please try again later."
			}, cancellationToken);

			return true;
		}
	}
}
