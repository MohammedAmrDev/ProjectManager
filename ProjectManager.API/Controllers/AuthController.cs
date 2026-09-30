using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Interfaces.IServices;
using ProjectManager.Application.DTOs;
using ProjectManager.Domain.Common.Result;
using ProjectManager.API.Extensions;

namespace ProjectManager.API.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class AuthController(IAuthService authService) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Register(RegisterRequest registerRequest)
		{
			Result loginResponse = await authService.Register(registerRequest);
			return loginResponse.IsSuccess ? Ok("Account created succesfully") : loginResponse.ToProblemDetailsResult();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			Result<AuthenticationResponse> authResponseResult = await authService.Login(loginRequest);
			return authResponseResult.IsSuccess ? Ok(authResponseResult.Value) : authResponseResult.ToProblemDetailsResult();
		}

		[HttpPost]
		public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
		{
			Result<AuthenticationResponse> authResponseResult = await authService.RefreshToken(refreshToken);
			return authResponseResult.IsSuccess ? Ok(authResponseResult.Value) : authResponseResult.ToProblemDetailsResult();
		}
	}
}
