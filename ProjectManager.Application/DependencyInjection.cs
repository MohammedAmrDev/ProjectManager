using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Behaviors;
using ProjectManager.Application.Interfaces.IServices;
using ProjectManager.Application.Services;

namespace ProjectManager.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddValidatorsFromAssembly(typeof(LoggingBehavior<,>).Assembly);
			services.AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(LoggingBehavior<,>).Assembly));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			services.AddScoped<IJwtService, JwtService>();

			return services;
		}
	}
}
