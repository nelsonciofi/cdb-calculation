using B3.Shared.Results;

namespace B3.Tests.Shared;

public class ValidationErrorTests
{
    [Fact]
    public void Constructor_ValidInput_SetsProperties()
    {
        string propertyName = "FirstName";
        string message = "Field is required.";

        var error = new ValidationError(propertyName, message);

        Assert.Equal(propertyName, error.PropertyName);
        Assert.Equal(message, error.Message);
    }

    [Theory]
    [InlineData(null, "Field is required.")]
    [InlineData("", "Field is required.")]
    [InlineData("  ", "Field is required.")]
    [InlineData("FirstName", null)]
    [InlineData("FirstName", "")]
    [InlineData(null, null)]
    [InlineData("", "")]
    public void Constructor_InvalidInput_ThrowsArgumentException(string propertyName, string message)
    {
        Assert.Throws<ArgumentException>(() => new ValidationError(propertyName, message));
    }
}
