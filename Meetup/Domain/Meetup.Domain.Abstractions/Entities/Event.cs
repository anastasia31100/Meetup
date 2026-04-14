using Meetup.ValueObjects;

namespace Meetup.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public Guid OrganizerId { get; private set; }
    public Guid EventTypeId { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventDate Date { get; private set; }
    public EventLocation Location { get; private set; }
    public MaxAttendees MaxAttendees { get; private set; }
    public int CurrentAttendees { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Навигационные свойства
    public virtual Organizer Organizer { get; private set; }
    public virtual EventType EventType { get; private set; }
    public virtual ICollection<Registration> Registrations { get; private set; }

    protected Event() { }

    public Event(
        Guid organizerId,
        Guid eventTypeId,
        EventTitle title,
        EventDescription description,
        EventDate date,
        EventLocation location,
        MaxAttendees maxAttendees)
    {
        Id = Guid.NewGuid();
        OrganizerId = organizerId;
        EventTypeId = eventTypeId;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Date = date ?? throw new ArgumentNullException(nameof(date));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        MaxAttendees = maxAttendees ?? throw new ArgumentNullException(nameof(maxAttendees));
        CurrentAttendees = 0;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        Registrations = new List<Registration>();
    }

    public bool HasAvailableSeats() => CurrentAttendees < MaxAttendees.Value;
    public int GetAvailableSeats() => MaxAttendees.Value - CurrentAttendees;

    public void RegisterAttendee()
    {
        if (!HasAvailableSeats())
            throw new InvalidOperationException("Нет свободных мест");
        CurrentAttendees++;
    }

    public void CancelRegistration()
    {
        if (CurrentAttendees > 0)
            CurrentAttendees--;
    }

    public void CancelEvent() => IsActive = false;
}