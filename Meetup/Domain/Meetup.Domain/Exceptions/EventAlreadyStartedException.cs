
namespace Meetup.Domain.Exceptions;

public class EventAlreadyStartedException(Event eventObj)
    : InvalidOperationException($"Не удается изменить событие \"{eventObj.Title}\" ,потому что оно уже началось")
{
    public Event Event => eventObj;
}
