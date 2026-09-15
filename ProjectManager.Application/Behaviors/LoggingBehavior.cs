using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ProjectManager.Application.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			logger.LogInformation("Start timer for request \"{Name}\"...", request.GetType().Name);
			var timer = Stopwatch.StartNew();

			TResponse response = await next(cancellationToken);

			timer.Stop();
			logger.LogInformation("End timer for request \"{Name}\", it took " + timer.ElapsedMilliseconds + "ms", request.GetType().Name);

			return response;
		}
	}
}
