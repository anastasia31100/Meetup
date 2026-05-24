
namespace Meetup.Domain.Exceptions;

public class AnotherOrganizerCancelEventException(Event eventObj, Organizer organizer)
    : InvalidOperationException($"Организатор {organizer.Username} не может отменить мероприятие \"{eventObj.Title}\" принадлежащее {eventObj.Organizer.Username}")
{
    public Event Event => eventObj;
    public Organizer Organizer => organizer;
}
