
using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Exceptions;

namespace Meetup.ValueObjects.Validators;

public class EntityTypeValidator : IValidator<string>
{
    private static readonly HashSet<string> AllowedValues = new() { "individual", "company" };

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"Недопустимый тип организатора: {value}. Допустимые: individual, company");
    }
}