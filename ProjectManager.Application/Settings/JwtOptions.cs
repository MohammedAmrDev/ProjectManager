namespace ProjectManager.Domain.Common.Settings
{
	public class JwtOptions
	{
		public string Issuer { get; set; } = string.Empty;
		public string Audience { get; set; } = string.Empty;
		public int AccessTokenDurationInMins { get; set; }
		public int RefreshTokenDurationInDays { get; set; }
		public string SecretKey { get; set; } = string.Empty;

	}
}
