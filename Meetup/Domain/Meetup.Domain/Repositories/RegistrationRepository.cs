using Meetup.Domain.Entities;
using Meetup.Domain.Abstractions.Repositories;
using Meetup.Domain.Entities;

namespace Meetup.Domain.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly List<Registration> _registrations = new();

    public Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = _registrations.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Registration>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Registration>>(_registrations.ToList());
    }

    public Task AddAsync(Registration entity, CancellationToken cancellationToken = default)
    {
        _registrations.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Registration entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Registration entity, CancellationToken cancellationToken = default)
    {
        _registrations.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_registrations.Any(r => r.Id == id));
    }

    public Task<Registration?> GetByEventAndAttendeeAsync(Guid eventId, Guid attendeeId, CancellationToken cancellationToken = default)
    {
        var result = _registrations.FirstOrDefault(r => r.EventId == eventId && r.AttendeeId == attendeeId && !r.IsCancelled);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Registration>> GetRegistrationsByEventAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var result = _registrations.Where(r => r.EventId == eventId && !r.IsCancelled).ToList();
        return Task.FromResult<IReadOnlyList<Registration>>(result);
    }

    public Task<IReadOnlyList<Registration>> GetRegistrationsByAttendeeAsync(Guid attendeeId, CancellationToken cancellationToken = default)
    {
        var result = _registrations.Where(r => r.AttendeeId == attendeeId && !r.IsCancelled).ToList();
        return Task.FromResult<IReadOnlyList<Registration>>(result);
    }

    public Task<bool> IsAttendeeRegisteredAsync(Guid eventId, Guid attendeeId, CancellationToken cancellationToken = default)
    {
        var isRegistered = _registrations.Any(r => r.EventId == eventId && r.AttendeeId == attendeeId && !r.IsCancelled);
        return Task.FromResult(isRegistered);
    }

    public Task<int> GetActiveRegistrationsCountAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var count = _registrations.Count(r => r.EventId == eventId && !r.IsCancelled);
        return Task.FromResult(count);
    }
}