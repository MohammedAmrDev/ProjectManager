using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _http;
	public CurrentUserService(IHttpContextAccessor http) => _http = http;

	public string? UserId =>
		_http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

	public bool IsAdmin =>
		_http.HttpContext?.User.IsInRole("Admin") ?? false;
}