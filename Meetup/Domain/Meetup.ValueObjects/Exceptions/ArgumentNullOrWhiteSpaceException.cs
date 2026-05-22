namespace Meetup.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
    : ArgumentNullException(paramName, $"Параметр \"{paramName}\" не должен быть нулевым, пустым или состоять только из пробелов.");