using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for username.
/// </summary>
public class UsernameValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username cannot be empty", nameof(value));

        if (value.Length < 3)
            throw new ArgumentException("Username must be at least 3 characters", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("Username cannot exceed 50 characters", nameof(value));
    }
}