

namespace Meetup.Domain.Exceptions;

public class InvalidEntityTypeException(string entityType)
    : ArgumentException($"Тип объекта \"{entityType}\" недопустим. Должно быть 'individual' или 'company'")
{
    public string EntityType => entityType;
}