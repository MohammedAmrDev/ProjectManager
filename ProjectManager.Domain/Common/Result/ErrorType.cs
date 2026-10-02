namespace ProjectManager.Domain.Common.Result
{
	public enum ErrorType
	{
		None,
		BadRequest,
		Unauthorized,
		Forbidden,
		NotFound,
		Conflict,
		Validation,
	}
}
