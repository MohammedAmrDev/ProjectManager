using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.API.Handlers;
using ProjectManager.Application;
using ProjectManager.Domain.Common.Settings;
using ProjectManager.Infrastructure;
using ProjectManager.Infrastructure.Data;
using ProjectManager.Infrastructure.Identity.Entities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails(options =>
{
	options.CustomizeProblemDetails = context =>
	{
		context.ProblemDetails.Extensions.Add("timestamp", DateTime.UtcNow);
		context.ProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
	};
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure(builder.Configuration); // Repositories, DbContext and Identity
builder.Services.AddApplication(); // MediatR and FluentValidation

JwtOptions jwtOptions = builder.Configuration.GetSection("JwtSettings").Get<JwtOptions>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtOptions.Issuer,
			ValidateAudience = true,
			ValidAudience = jwtOptions.Audience,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
		};
	});


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

// Registering Identity
builder.Services.AddIdentityCore<ApplicationUser>()
	.AddRoles<ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddSignInManager();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



// Place this code block in Program.cs right BEFORE app.Run();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>()!;

    // 1. Seed Roles
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
    
    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new ApplicationRole { Name = "User" });
}

app.Run();



app.Run();
