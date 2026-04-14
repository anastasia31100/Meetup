using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for company name.
/// </summary>
public class CompanyNameValidator : IValidator<string?>
{
    public void Validate(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 200)
            throw new ArgumentException("Company name cannot exceed 200 characters", nameof(value));
    }
}