using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Behaviors;
using ProjectManager.Infrastructure;
using ProjectManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// EntityFramework
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR and FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(LoggingBehavior<,>).Assembly);
builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(LoggingBehavior<,>).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "Project Manager Web API");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
