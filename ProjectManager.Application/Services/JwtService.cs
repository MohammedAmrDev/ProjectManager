using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Application.DTOs;
using ProjectManager.Application.Interfaces.IServices;
using ProjectManager.Domain.Common.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.Application.Services
{
	public class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
	{
		public string GenerateToken(AuthenticationDTO authenticationDTO)
		{
			Claim[] claimsList = [.. authenticationDTO.Roles.Select(r => new Claim(ClaimTypes.Role, r))];
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = jwtOptions.Value.Issuer,
				Audience = jwtOptions.Value.Audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)), SecurityAlgorithms.HmacSha256),
				Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenDurationInMins),
				Subject = new ClaimsIdentity([
					new(ClaimTypes.NameIdentifier, authenticationDTO.Id.ToString()),
					new(ClaimTypes.Email, authenticationDTO.Email),
					new(ClaimTypes.Name, authenticationDTO.FullName),
					..claimsList,
				]),
			};

			var securtyToken = tokenHandler.CreateToken(tokenDescriptor);
			var accessToken = tokenHandler.WriteToken(securtyToken);

			return accessToken;
		}

		public (string Token, string HashedToken, DateTime ExpirationDate) GenerateRefreshToken()
		{
			var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
			var hashedToken = HashToken(token);
			var expirationDate = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDurationInDays);

			return (token, hashedToken, expirationDate);
		}

		public string HashToken(string token) =>
			Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
	}
}
