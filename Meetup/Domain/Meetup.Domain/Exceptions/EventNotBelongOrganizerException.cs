
namespace Meetup.Domain.Exceptions;

public class EventNotBelongOrganizerException(Event eventObj, Organizer organizer)
    : InvalidOperationException($"Событие \"{eventObj.Title}\" не принадлежит организатору {organizer.Username}")
{
    public Event Event => eventObj;
    public Organizer Organizer => organizer;
}
