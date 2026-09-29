using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Interfaces.IRepositories;
using ProjectManager.Domain.User;
using ProjectManager.Infrastructure.Data;

namespace ProjectManager.Infrastructure.Repositories
{
	public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
	{
		public void Add(RefreshToken refreshToken) =>
			context.RefreshTokens.Add(refreshToken);
		public void Update(RefreshToken refreshToken) =>
			context.RefreshTokens.Update(refreshToken);

		public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token) =>
			await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

	}
}
