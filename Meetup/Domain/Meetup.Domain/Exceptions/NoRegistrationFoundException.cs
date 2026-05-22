
namespace Meetup.Domain.Exceptions;

public class NoRegistrationFoundException(Attendee attendee, Event eventObj)
    : InvalidOperationException($"Не найдена активная регистрация участника  {attendee.Username} на событие \"{eventObj.Title}\"")
{
    public Attendee Attendee => attendee;
    public Event Event => eventObj;
}
