
using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;

namespace Meetup.Domain;

public class Event : Entity<Guid>
{
    private readonly List<Registration> _registrations = [];

    public Organizer Organizer { get; private set; }
    public EventType EventType { get; private set; }
    public Title Title { get; private set; }
    public EventDescription Description { get; private set; }
    public DateTime EventDate { get; private set; }
    public Location Location { get; private set; }
    public int MaxAttendees { get; private set; }
    public int CurrentAttendees { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsCancelled { get; private set; }
    public IReadOnlyCollection<Registration> Registrations => _registrations.AsReadOnly();

    protected Event() { }

    public Event(Guid id, Organizer organizer, EventType eventType, Title title,
        EventDescription description, DateTime eventDate, Location location,
        int maxAttendees) : base(id)
    {
        Organizer = organizer ?? throw new ArgumentNullValueException(nameof(organizer));
        EventType = eventType ?? throw new ArgumentNullValueException(nameof(eventType));
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Description = description;
        EventDate = eventDate;
        Location = location ?? throw new ArgumentNullValueException(nameof(location));

        if (maxAttendees < 1)
            throw new InvalidMaxAttendeesException(maxAttendees);

        MaxAttendees = maxAttendees;
        CurrentAttendees = 0;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    public Registration RegisterAttendee(Attendee attendee)
    {
        if (attendee == null) throw new ArgumentNullValueException(nameof(attendee));

        if (!IsActive || IsCancelled)
            throw new EventNotActiveException(this);

        if (Started())
            throw new EventAlreadyStartedException(this);

        if (CurrentAttendees >= MaxAttendees)
            throw new EventFullException(this);

        var registration = new Registration(Guid.NewGuid(), this, attendee);
        _registrations.Add(registration);
        CurrentAttendees++;

        return registration;
    }

    internal bool UpdateDetails(Title newTitle, EventDescription newDescription,
        DateTime newEventDate, Location newLocation, int newMaxAttendees)
    {
        bool updated = false;

        if (!Title.Equals(newTitle))
        {
            Title = newTitle;
            updated = true;
        }

        if (!Equals(Description, newDescription))
        {
            Description = newDescription;
            updated = true;
        }

        if (!EventDate.Equals(newEventDate))
        {
            if (newEventDate < DateTime.UtcNow)
                throw new InvalidEventDateException(newEventDate);
            EventDate = newEventDate;
            updated = true;
        }

        if (!Location.Equals(newLocation))
        {
            Location = newLocation;
            updated = true;
        }

        if (MaxAttendees != newMaxAttendees)
        {
            if (newMaxAttendees < CurrentAttendees)
                throw new InvalidMaxAttendeesException(newMaxAttendees, CurrentAttendees);
            MaxAttendees = newMaxAttendees;
            updated = true;
        }

        return updated;
    }

    internal void Cancel()
    {
        if (Started())
            throw new EventAlreadyStartedException(this);

        IsActive = false;
        IsCancelled = true;

        foreach (var registration in _registrations.Where(r => !r.IsCancelled))
        {
            registration.Cancel();
        }
    }

    internal void RemoveRegistration(Registration registration)
    {
        if (_registrations.Contains(registration) && !registration.IsCancelled)
        {
            CurrentAttendees--;
        }
    }

    public bool Started() => EventDate < DateTime.UtcNow;

    public int AvailableSeats() => MaxAttendees - CurrentAttendees;

    public bool ChangeEventType(EventType newEventType)
    {
        if (newEventType == null) throw new ArgumentNullValueException(nameof(newEventType));
        if (Started()) throw new EventAlreadyStartedException(this);

        if (EventType.Id == newEventType.Id) return false;

        EventType = newEventType;
        return true;
    }
}