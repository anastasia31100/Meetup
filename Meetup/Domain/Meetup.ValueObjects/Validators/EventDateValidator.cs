using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for event date.
/// </summary>
public class EventDateValidator : IValidator<DateTime>
{
    public void Validate(DateTime value)
    {
        if (value < DateTime.Now)
            throw new ArgumentException("Event date cannot be in the past", nameof(value));
    }
}