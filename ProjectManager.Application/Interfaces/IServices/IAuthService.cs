using ProjectManager.Application.DTOs;
using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Application.Interfaces.IServices
{
	public interface IAuthService
	{
		Task<Result> Register(RegisterRequest registerRequest);
		Task<Result<AuthenticationResponse>> Login(LoginRequest loginRequest);
		Task<Result<AuthenticationResponse>> RefreshToken(string oldRefreshToken);
	}
}
