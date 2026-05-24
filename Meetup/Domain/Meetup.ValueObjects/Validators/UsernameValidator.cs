using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Exceptions;

namespace Meetup.ValueObjects.Validators;
public class UsernameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 50;
    public static int MIN_LENGTH => 3;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }

}