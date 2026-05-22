
namespace Meetup.Domain.Exceptions;

public class AnotherOrganizerEditEventException(Event eventObj, Organizer organizer)
    : InvalidOperationException($"Организатор {organizer.Username} не может редактировать событие  \"{eventObj.Title}\" принадлежащее  {eventObj.Organizer.Username}")
{
    public Event Event => eventObj;
    public Organizer Organizer => organizer;
}
