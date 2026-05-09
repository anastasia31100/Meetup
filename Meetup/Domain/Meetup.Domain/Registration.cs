
using Meetup.Domain.Exceptions;
using Meetup.Domain.Base;

namespace Meetup.Domain;

public class Registration : Entity<Guid>
{
    public Event Event { get; private set; }
    public Attendee Attendee { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public bool IsCancelled { get; private set; }

    protected Registration() { }

    public Registration(Guid id, Event eventObj, Attendee attendee) : base(id)
    {
        Event = eventObj ?? throw new ArgumentNullValueException(nameof(eventObj));
        Attendee = attendee ?? throw new ArgumentNullValueException(nameof(attendee));
        RegisteredAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    internal bool Cancel()
    {
        if (IsCancelled) return false;

        if (Event.Started())
            throw new EventAlreadyStartedException(Event);

        IsCancelled = true;
        Event.RemoveRegistration(this);
        return true;
    }
}