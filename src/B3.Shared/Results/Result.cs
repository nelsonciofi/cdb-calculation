namespace B3.Shared.Results;

public readonly struct Result<TSuccess, TFail>
{
    private readonly TSuccess? successValue;
    private readonly TFail? failValue;

    private readonly bool isSuccess;

    public Result(TSuccess successValue)
    {
        this.successValue = successValue;
        failValue = default;
        isSuccess = true;
    }

    public Result(TFail failValue)
    {
        successValue = default;
        this.failValue = failValue;
        isSuccess = false;
    }

    public readonly bool IsSuccess => isSuccess;
    public readonly bool IsFail => !isSuccess;


    public static implicit operator Result<TSuccess, TFail>(TSuccess success) => new(success);
    public static implicit operator Result<TSuccess, TFail>(TFail fail) => new(fail);

    public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess,
                                  Func<TFail, TResult> onFail)
    {
        if (isSuccess && successValue is not null)
        {
            return onSuccess(successValue);
        }
        else if (failValue is not null)
        {
            return onFail(failValue);
        }
        else
        {
            throw new InvalidOperationException("The result has not been correctly initialized.");
        }
    }
}
