namespace ProjectManager.Domain.Common.Result
{
	public enum ErrorType
	{
		None,
		NotFound,
		Unauthorized,
		Validation,
		Conflict,
		BadRequest,
		Forbidden,
	}
}
