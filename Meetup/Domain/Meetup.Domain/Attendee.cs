using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;


namespace Meetup.Domain;

public class Attendee : Entity<Guid>
{
    private readonly List<Registration> _registrations = [];

    public Username Username { get; private set; }
    public IReadOnlyCollection<Registration> Registrations => _registrations.AsReadOnly();
    public IReadOnlyCollection<Registration> ActiveRegistrations =>
        _registrations.Where(r => !r.IsCancelled && !r.Event.IsCancelled && !r.Event.Started()).ToList().AsReadOnly();
    public IReadOnlyCollection<Registration> History =>
        _registrations.Where(r => r.IsCancelled || r.Event.Started() || r.Event.IsCancelled).ToList().AsReadOnly();

    protected Attendee() { }

    public Attendee(Guid id, Username username) : base(id)
    {
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
    }

    public Registration RegisterForEvent(Event eventToRegister)
    {
        if (eventToRegister == null) throw new ArgumentNullValueException(nameof(eventToRegister));

        var existingRegistration = _registrations.FirstOrDefault(r => r.Event.Id == eventToRegister.Id && !r.IsCancelled);
        if (existingRegistration != null)
            throw new AlreadyRegisteredException(this, eventToRegister);

        var registration = eventToRegister.RegisterAttendee(this);
        _registrations.Add(registration);
        return registration;
    }

    public bool CancelRegistration(Event eventToCancel)
    {
        if (eventToCancel == null) throw new ArgumentNullValueException(nameof(eventToCancel));

        var registration = _registrations.FirstOrDefault(r => r.Event.Id == eventToCancel.Id && !r.IsCancelled);
        if (registration == null)
            throw new NoRegistrationFoundException(this, eventToCancel);

        if (eventToCancel.Started())
            throw new EventAlreadyStartedException(eventToCancel);

        return registration.Cancel();
    }

    public bool ChangeUsername(Username newUsername)
    {
        if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));
        if (Username.Equals(newUsername)) return false;
        Username = newUsername;
        return true;
    }
}