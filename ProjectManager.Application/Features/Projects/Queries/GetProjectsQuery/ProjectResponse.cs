namespace ProjectManager.Application.Features.Projects.Queries.GetProjectsQuery
{
	public sealed record ProjectResponse(string Name, DateTimeOffset CreatedAt);
}