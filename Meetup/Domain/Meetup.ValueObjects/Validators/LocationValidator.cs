using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for event location.
/// </summary>
public class LocationValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Location cannot be empty", nameof(value));

        if (value.Length > 200)
            throw new ArgumentException("Location cannot exceed 200 characters", nameof(value));
    }
}