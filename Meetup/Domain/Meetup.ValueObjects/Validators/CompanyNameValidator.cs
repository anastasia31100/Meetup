using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Exceptions;

namespace Meetup.ValueObjects.Validators;

public class CompanyNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 200;

    public void Validate(string value)
    {
        if (value != null && value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}