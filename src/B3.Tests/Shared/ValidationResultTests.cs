using B3.Shared.Results;

namespace B3.Tests.Shared;

public class ValidationResultTests
{
    [Fact]
    public void IsValid_NoErrors_ReturnsTrue()
    {
        var validationResult = new ValidationResult();

        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void IsValid_WithErrors_ReturnsFalse()
    {
        var validationResult = new ValidationResult();
        validationResult.AddError("Name", "Field is required.");

        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public void AddError_ValidInput_AddsError()
    {
        var validationResult = new ValidationResult();
        var error = new ValidationError("Age", "Invalid age.");

        validationResult.AddError(error);

        Assert.Single(validationResult.Errors);
        Assert.Equal(error, validationResult.Errors.Single());
    }

    [Fact]
    public void AddError_StringInput_AddsError()
    {
        var validationResult = new ValidationResult();

        validationResult.AddError("Email", "Invalid email.");

        Assert.Single(validationResult.Errors);
        Assert.Equal("Email", validationResult.Errors.Single().PropertyName);
        Assert.Equal("Invalid email.", validationResult.Errors.Single().Message);
    }

    [Fact]
    public void AddError_NullPropertyName_ThrowsArgumentException()
    {
        var validationResult = new ValidationResult();

        Assert.Throws<ArgumentException>(() => validationResult.AddError(null!, "Invalid."));
    }

    [Fact]
    public void AddError_EmptyPropertyName_ThrowsArgumentException()
    {
        var validationResult = new ValidationResult();

        Assert.Throws<ArgumentException>(() => validationResult.AddError("", "Invalid."));
    }

    [Fact]
    public void AddError_NullMessage_ThrowsArgumentException()
    {
        var validationResult = new ValidationResult();

        Assert.Throws<ArgumentException>(() => validationResult.AddError("Phone", null!));
    }

    [Fact]
    public void AddError_EmptyMessage_ThrowsArgumentException()
    {
        var validationResult = new ValidationResult();

        Assert.Throws<ArgumentException>(() => validationResult.AddError("Phone", ""));
    }
}
