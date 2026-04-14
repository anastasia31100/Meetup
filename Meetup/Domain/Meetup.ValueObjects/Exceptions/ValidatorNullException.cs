namespace Meetup.ValueObjects.Exceptions;

public class ValidatorNullException : ArgumentNullException
{
    public ValidatorNullException(string paramName) : base(paramName) { }
}