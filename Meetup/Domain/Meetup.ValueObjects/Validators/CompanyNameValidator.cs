using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Exceptions;

namespace Meetup.ValueObjects.Validators;

public class CompanyNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 200;

    public void Validate(string? value)
    {
        // null разрешён (компания может не указываться)
        if (value == null)
            return;

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}