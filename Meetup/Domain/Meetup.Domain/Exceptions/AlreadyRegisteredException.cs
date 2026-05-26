
namespace Meetup.Domain.Exceptions;

public class AlreadyRegisteredException(Attendee attendee, Event eventObj)
    : InvalidOperationException($"Участник {attendee.Username} уже зарегистрирован для участия в мероприятии \"{eventObj.Title}\"")
{
    public Attendee Attendee => attendee;
    public Event Event => eventObj;
}
