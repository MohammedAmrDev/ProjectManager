using ProjectManager.Domain.User;

namespace ProjectManager.Application.Interfaces.IRepositories
{
	public interface IRefreshTokenRepository
	{
		void Add(RefreshToken refreshToken);
		void Update(RefreshToken refreshToken);
		Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
	}
}
