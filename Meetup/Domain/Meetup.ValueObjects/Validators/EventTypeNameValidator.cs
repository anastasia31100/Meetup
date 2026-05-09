using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Exceptions;
namespace Meetup.ValueObjects.Validators;

public class EventTypeNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 100;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}