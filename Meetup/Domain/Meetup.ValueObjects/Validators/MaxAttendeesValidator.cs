using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for max attendees.
/// </summary>
public class MaxAttendeesValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value < 1)
            throw new ArgumentException("Max attendees must be at least 1", nameof(value));

        if (value > 10000)
            throw new ArgumentException("Max attendees cannot exceed 10000", nameof(value));
    }
}