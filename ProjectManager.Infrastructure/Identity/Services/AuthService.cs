using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.DTOs;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Application.Interfaces.IServices;
using ProjectManager.Domain.Common.Result;
using ProjectManager.Domain.RefreshToken;
using ProjectManager.Domain.User;
using ProjectManager.Infrastructure.Identity.Entities;

namespace ProjectManager.Infrastructure.Identity.Services
{
	internal class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork uow, IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository) : IAuthService
	{
		public async Task<Result> Register(RegisterRequest registerRequest)
		{
			ApplicationUser applicationUser = new ApplicationUser
			{
				FirstName = registerRequest.FirstName,
				LastName = registerRequest.LastName,
				Email = registerRequest.Email,
				UserName = registerRequest.Email.Split('@')[0],
			};

			using var transaction = uow.BeginTransaction();

			var identityResult = await userManager.CreateAsync(applicationUser, registerRequest.Password);

			if (!identityResult.Succeeded)
				return UserErrors.RegisterationFailed;

			var addToRoleResult = await userManager.AddToRoleAsync(applicationUser, UserRole.User.ToString());

			if (!addToRoleResult.Succeeded)
				return UserErrors.RegisterationFailed;

			transaction.Commit();

			return new Result();
		}

		public async Task<Result<AuthenticationResponse>> Login(LoginRequest loginRequest)
		{
			ApplicationUser? user = await userManager.FindByEmailAsync(loginRequest.Email);

			if (user is null)
				return UserErrors.UserNotFound;

			if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.UtcNow)
				return UserErrors.LockedUser;

			SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: true);

			if (!signInResult.Succeeded)
				return UserErrors.LoginFailed;

			var userRoles = await userManager.GetRolesAsync(user);
			AuthenticationDTO authenticationDTO = new AuthenticationDTO(user.Id, user.Email!, user.FirstName + " " + user.LastName, [.. userRoles]);

			// Generate and save refresh token
			var (token, hashedToken, expirationDate) = jwtService.GenerateRefreshToken();

			var refreshTokenEntity = new RefreshToken
			{
				Id = Guid.CreateVersion7(),
				Token = hashedToken,
				ExpiresOnUtc = expirationDate,
				UserId = user.Id,
			};

			refreshTokenRepository.Add(refreshTokenEntity);
			await uow.SaveChangesAsync();

			return new AuthenticationResponse(jwtService.GenerateToken(authenticationDTO), token);
		}

		public async Task<Result<AuthenticationResponse>> RefreshToken(string oldRefreshToken)
		{
			var oldRefreshTokenEntity = await refreshTokenRepository.GetRefreshTokenByTokenAsync(jwtService.HashToken(oldRefreshToken));
			if (oldRefreshTokenEntity is null)
				return TokenErrors.RefreshTokenNotFound;

			if (DateTime.UtcNow >= oldRefreshTokenEntity.ExpiresOnUtc)
				return TokenErrors.RefreshTokenExpired;

			ApplicationUser? user = await userManager.FindByIdAsync(oldRefreshTokenEntity.UserId.ToString());
			if (user is null)
				return UserErrors.UserNotFound;

			var (token, hashedToken, expirationDate) = jwtService.GenerateRefreshToken();

			oldRefreshTokenEntity.Token = hashedToken;
			oldRefreshTokenEntity.ExpiresOnUtc = expirationDate;

			refreshTokenRepository.Update(oldRefreshTokenEntity);
			await uow.SaveChangesAsync();

			var userRoles = await userManager.GetRolesAsync(user);
			AuthenticationDTO authenticationDTO = new AuthenticationDTO(user.Id, user.Email!, user.FirstName + " " + user.LastName, [.. userRoles]);

			return new AuthenticationResponse(jwtService.GenerateToken(authenticationDTO), token);
		}
	}
}
