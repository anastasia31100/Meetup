using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for event title.
/// </summary>
public class TitleValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty", nameof(value));

        if (value.Length < 3)
            throw new ArgumentException("Title must be at least 3 characters", nameof(value));

        if (value.Length > 200)
            throw new ArgumentException("Title cannot exceed 200 characters", nameof(value));
    }
}