using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;

namespace Meetup.Domain;

public class Event : Entity<Guid>
{
    private readonly ICollection<Registration> _registrations = new List<Registration>();

    public Organizer Organizer { get; private set; }
    public EventType EventType { get; private set; }
    public Title Title { get; private set; }
    public EventDescription Description { get; private set; }
    public DateTime EventDate { get; private set; }
    public Location Location { get; private set; }
    public SeatCount MaxAttendees { get; private set; } // теперь Value Object
    public SeatCount CurrentAttendees { get; private set; } // теперь Value Object
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsCancelled { get; private set; }

    public IReadOnlyCollection<Registration> Registrations => _registrations.ToList().AsReadOnly();

    protected Event() { }

    protected Event(Guid id, Organizer organizer, EventType eventType, Title title,
        EventDescription description, DateTime eventDate, Location location, SeatCount maxAttendees)
        : base(id)
    {
        Organizer = organizer ?? throw new ArgumentNullValueException(nameof(organizer));
        EventType = eventType ?? throw new ArgumentNullValueException(nameof(eventType));
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Description = description;
        EventDate = eventDate;
        Location = location ?? throw new ArgumentNullValueException(nameof(location));
        MaxAttendees = maxAttendees ?? throw new ArgumentNullValueException(nameof(maxAttendees));

        if (MaxAttendees.Value < 1)
            throw new InvalidMaxAttendeesException(MaxAttendees.Value);

        CurrentAttendees = new SeatCount(0);
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    // Публичный конструктор для удобства (с int для maxAttendees)
    public Event(Organizer organizer, EventType eventType, Title title,
        EventDescription description, DateTime eventDate, Location location, int maxAttendees)
        : this(Guid.NewGuid(), organizer, eventType, title, description, eventDate, location, new SeatCount(maxAttendees))
    {
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

        var registration = new Registration(this, attendee); // конструктор Registration сгенерирует Guid
        _registrations.Add(registration);
        CurrentAttendees = new SeatCount(CurrentAttendees.Value + 1);
        return registration;
    }

    // Метод для отмены регистрации с проверкой, что отменяет тот же участник
    internal void RemoveRegistration(Registration registration, Attendee requester)
    {
        if (registration.Attendee != requester)
            throw new InvalidOperationException("Только участник может отменить свою регистрацию");

        if (_registrations.Contains(registration) && !registration.IsCancelled)
        {
            CurrentAttendees = new SeatCount(CurrentAttendees.Value - 1);
        }
    }

    internal bool UpdateDetails(Organizer requester, Title newTitle, EventDescription newDescription,
        DateTime newEventDate, Location newLocation, int newMaxAttendees)
    {
        if (requester != Organizer)
            throw new AnotherOrganizerEditEventException(this, requester);

        bool updated = false;

        if (Title != newTitle) // используем оператор !=
        {
            Title = newTitle;
            updated = true;
        }

        if (Description != newDescription) // оператор !=
        {
            Description = newDescription;
            updated = true;
        }

        if (EventDate != newEventDate) // DateTime также поддерживает !=
        {
            if (newEventDate < DateTime.UtcNow)
                throw new InvalidEventDateException(newEventDate);
            EventDate = newEventDate;
            updated = true;
        }

        if (Location != newLocation) // оператор !=
        {
            Location = newLocation;
            updated = true;
        }

        var newMax = new SeatCount(newMaxAttendees);
        if (MaxAttendees != newMax)
        {
            if (newMax < CurrentAttendees)
                throw new InvalidMaxAttendeesException(newMax.Value, CurrentAttendees.Value);
            MaxAttendees = newMax;
            updated = true;
        }

        return updated;
    }

    internal void Cancel(Organizer requester)
    {
        if (requester != Organizer)
            throw new AnotherOrganizerCancelEventException(this, requester);

        if (Started())
            throw new EventAlreadyStartedException(this);

        IsActive = false;
        IsCancelled = true;

        foreach (var registration in _registrations.Where(r => !r.IsCancelled))
        {
            registration.Cancel(requester); 
        }
    }
    public int AvailableSeats()
    {
        return MaxAttendees.Value - CurrentAttendees.Value;
    }
    public bool Started() => EventDate < DateTime.UtcNow;
}