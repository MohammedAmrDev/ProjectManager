namespace ProjectManager.Application.DTOs
{
	public sealed record AuthenticationResponse(string AccessToken, string RefreshToken);
}