
namespace Meetup.Domain.Exceptions;

public class InvalidMaxAttendeesException(int maxAttendees, int? currentAttendees = null)
    : ArgumentException(currentAttendees.HasValue
        ? $"Максимальное количество участников  ({maxAttendees}) не может быть меньше текущего количества участников ({currentAttendees.Value})"
        : $"Максимальное количество участников  ({maxAttendees}) должно быть не менее 1")
{
    public int MaxAttendees => maxAttendees;
    public int? CurrentAttendees => currentAttendees;
}
