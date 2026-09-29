namespace ProjectManager.Application.DTOs
{
	public sealed record AuthenticationDTO(Guid Id, string Email, string FullName, string[] Roles);
}