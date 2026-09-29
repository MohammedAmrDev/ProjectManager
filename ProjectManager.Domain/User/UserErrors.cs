using ProjectManager.Domain.Common.Result;

namespace ProjectManager.Domain.User
{
	public static class UserErrors
	{
		public static Error RegisterationFailed = new("User.RegisterationFailed", "Failed to register", ErrorType.BadRequest);
		public static Error UserNotFound = new("User.UserNotFound", "User not found", ErrorType.Unauthorized);
		public static Error LockedUser = new("User.LockedUser", "User is locked", ErrorType.Forbidden);
		public static Error LoginFailed = new("User.LoginFailed", "Failed to login", ErrorType.Unauthorized);
	}
}