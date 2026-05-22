
namespace Meetup.Domain.Exceptions;

public class ArgumentNullValueException : ArgumentNullException
{
    public ArgumentNullValueException(string paramName)
        : base(paramName, $"Параметр \"{paramName}\" не может быть null.")
    {
    }

    public ArgumentNullValueException(string paramName, string message)
        : base(paramName, message)
    {
    }
}