namespace ProjectManager.Domain.Common.Result
{
	public sealed record Error(string Code, string Description, ErrorType ErrorType)
	{
		public static Error None => new(string.Empty, string.Empty, ErrorType.None);
	}
}