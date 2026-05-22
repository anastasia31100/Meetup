
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

    protected Registration(Guid id, Event eventObj, Attendee attendee) : base(id)
    {
        Event = eventObj ?? throw new ArgumentNullValueException(nameof(eventObj));
        Attendee = attendee ?? throw new ArgumentNullValueException(nameof(attendee));
        RegisteredAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    // Публичный конструктор
    public Registration(Event eventObj, Attendee attendee) : this(Guid.NewGuid(), eventObj, attendee)
    {
    }

    // Отмена регистрации самим участником
    public bool Cancel(Attendee requester)
    {
        if (requester != Attendee)
            throw new InvalidOperationException("Только участник может отменить свою регистрацию");

        if (IsCancelled) return false;

        if (Event.Started())
            throw new EventAlreadyStartedException(Event);

        IsCancelled = true;
        Event.RemoveRegistration(this, requester);
        return true;
    }

    // Отмена регистрации при отмене мероприятия (вызывается организатором)
    internal void Cancel()
    {
        if (IsCancelled) return;
        IsCancelled = true;
        // Здесь не нужно вызывать Event.RemoveRegistration, потому что мероприятие уже отменяется
    }
    internal void Cancel(Organizer requester)
    {
        if (requester != Event.Organizer)
            throw new AnotherOrganizerCancelEventException(Event, requester);

        if (IsCancelled) return;
        IsCancelled = true;
        // Счётчик не уменьшаем, так как всё мероприятие отменяется
    }
}