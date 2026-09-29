using ProjectManager.Application.DTOs;
using ProjectManager.Domain.User;

namespace ProjectManager.Application.Interfaces.IServices
{
	public interface IJwtService
	{
		string GenerateToken(AuthenticationDTO authenticationDTO);
		(string Token, string HashedToken, DateTime ExpirationDate) GenerateRefreshToken();
		string HashToken(string token);
	}
}