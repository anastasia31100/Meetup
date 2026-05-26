
namespace Meetup.Domain.Exceptions;

public class EventNotActiveException(Event eventObj)
    : InvalidOperationException($"Событие \"{eventObj.Title}\" неактивно или было отменено")
{
    public Event Event => eventObj;
}
