using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for event description.
/// </summary>
public class DescriptionValidator : IValidator<string?>
{
    public void Validate(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 4000)
            throw new ArgumentException("Description cannot exceed 4000 characters", nameof(value));
    }
}