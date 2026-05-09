
namespace Meetup.Domain.Exceptions;

public class EventFullException(Event eventObj)
    : InvalidOperationException($"Событие \"{eventObj.Title}\" достигло максимальной вместимости  ({eventObj.MaxAttendees} участников)")
{
    public Event Event => eventObj;
}
