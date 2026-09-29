using Microsoft.AspNetCore.Identity;

namespace ProjectManager.Infrastructure.Identity.Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
	}
}
