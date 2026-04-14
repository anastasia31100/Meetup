namespace Meetup.Domain.Entities;

public class Registration
{
    public Guid Id { get; private set; }
    public Guid EventId { get; private set; }
    public Guid AttendeeId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public bool IsCancelled { get; private set; }

    public Event Event { get; private set; }
    public Attendee Attendee { get; private set; }

    private Registration() { }

    public Registration(Guid eventId, Guid attendeeId)
    {
        Id = Guid.NewGuid();
        EventId = eventId;
        AttendeeId = attendeeId;
        RegisteredAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    public void Cancel()
    {
        IsCancelled = true;
    }
}