
namespace Meetup.Domain.Exceptions;

public class InvalidEventDateException(DateTime eventDate)
    : ArgumentException($"Дата события {eventDate} не может быть в прошлом")
{
    public DateTime EventDate => eventDate;
}
