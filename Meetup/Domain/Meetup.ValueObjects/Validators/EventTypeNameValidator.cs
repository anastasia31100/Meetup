using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for event type name.
/// </summary>
public class EventTypeNameValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Event type name cannot be empty", nameof(value));

        if (value.Length < 2)
            throw new ArgumentException("Event type name must be at least 2 characters", nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Event type name cannot exceed 100 characters", nameof(value));
    }
}