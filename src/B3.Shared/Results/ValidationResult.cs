namespace B3.Shared.Results;

public class ValidationResult
{
    private readonly List<ValidationError> errors = new();

    public IReadOnlyList<ValidationError> Errors => errors;

    public bool IsValid => errors.Count == 0;

    public void AddError(ValidationError error)
    {
        errors.Add(error);
    }

    public void AddError(string propertyName, string message)
    {
        errors.Add(new ValidationError(propertyName, message));
    }
}


