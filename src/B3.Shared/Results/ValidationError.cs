namespace B3.Shared.Results;

public sealed class ValidationError
{
    public string PropertyName { get; set; }
    public string Message { get; set; }

    public ValidationError(string propertyName, string message)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or whitespace.", nameof(propertyName));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
        }

        PropertyName = propertyName;
        Message = message;
    }
}