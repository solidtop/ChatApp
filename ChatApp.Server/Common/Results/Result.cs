namespace ChatApp.Server.Common.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Invalid error");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Ok() => new(true, Error.None);
    public static Result<T> Ok<T>(T value) => new(value, true, Error.None);
    public static Result Fail(Error error) => new(false, error);
    public static Result<T> Fail<T>(Error error) => new(default, false, error);
}

public class Result<T>(T? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    public T? Value { get; } = value;

    public static implicit operator Result<T>(T value) =>
        value != null ? Ok(value) : Fail<T>(Error.None);

    public static implicit operator Result<T>(Error error) =>
        Fail<T>(error);
}
