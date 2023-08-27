using B3.Shared.Results;

namespace B3.Tests.Shared;

public class ResultTests
{
    [Fact]
    public void IsSuccess_SuccessfulResult_ReturnsTrue()
    {
        var result = new Result<int, string>(10);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void IsSuccess_FailedResult_ReturnsFalse()
    {
        var result = new Result<int, string>("Error message");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void IsFail_SuccessfulResult_ReturnsFalse()
    {
        var result = new Result<int, string>(10);

        Assert.False(result.IsFail);
    }

    [Fact]
    public void IsFail_FailedResult_ReturnsTrue()
    {
        var result = new Result<int, string>("Error message");

        Assert.True(result.IsFail);
    }

    [Fact]
    public void Match_SuccessfulResult_CallsOnSuccessFunction()
    {
        var result = new Result<int, string>(10);
        bool onSuccessCalled = false;

        result.Match(
            onSuccess: _ => onSuccessCalled = true,
            onFail: _ => onSuccessCalled = false
        );

        Assert.True(onSuccessCalled);
    }

    [Fact]
    public void Match_FailedResult_CallsOnFailFunction()
    {
        var result = new Result<int, string>("Error message");
        bool onFailCalled = false;

        result.Match(
            onSuccess: _ => onFailCalled = false,
            onFail: _ => onFailCalled = true
        );

        Assert.True(onFailCalled);
    }

    [Fact]
    public void Match_UninitializedResult_ThrowsInvalidOperationException()
    {
        var uninitializedResult = new Result<int, string>();

        Assert.Throws<InvalidOperationException>(() =>
            uninitializedResult.Match<int>(
                onSuccess: (int _) => { return 1; },
                onFail: (string _) => { return 1; }
            )
        );
    }

}
