using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

/// <summary>
/// Validator for entity type (individual, company, sole_proprietor).
/// </summary>
public class EntityTypeValidator : IValidator<string>
{
    private static readonly string[] ValidTypes = { "individual", "company", "sole_proprietor" };

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Entity type cannot be empty", nameof(value));

        if (!ValidTypes.Contains(value.ToLower()))
            throw new ArgumentException($"Entity type must be one of: {string.Join(", ", ValidTypes)}", nameof(value));
    }
}
