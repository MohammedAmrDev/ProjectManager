using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Domain.RefreshToken
{
	public static class TokenErrors
	{
		public static Error RefreshTokenNotFound = new("TokenError.RefreshTokenNotFound", "Refresh token was not found", ErrorType.NotFound);
		public static Error RefreshTokenExpired = new("TokenError.RefreshTokenExpired", "Refresh token is expired", ErrorType.Unauthorized);
	}
}
