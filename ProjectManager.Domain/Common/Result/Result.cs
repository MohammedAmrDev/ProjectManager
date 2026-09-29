namespace ProjectManager.Domain.Common.Result
{
	public class Result
	{
		public bool IsSuccess { get; }
		public bool IsFailure => !IsSuccess;
		public Error Error { get; }

		public Result()
		{
			IsSuccess = true;
			Error = Error.None;
		}

		public Result(Error error)
		{
			IsSuccess = false;
			Error = error;
		}

		public static implicit operator Result(Error error) => new Result(error);
	}

	public class Result<TValue> : Result
	{
		public TValue? Value { get; }

		public Result(TValue value) : base()
		{
			Value = value;
		}

		public Result(Error error) : base(error)
		{
			Value = default;
		}

		public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(value);
		public static implicit operator Result<TValue>(Error error) => new Result<TValue>(error);
	}
}
