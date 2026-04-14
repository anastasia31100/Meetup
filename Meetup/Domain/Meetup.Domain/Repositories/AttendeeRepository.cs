using Meetup.Domain.Entities;
using Meetup.Domain.Abstractions.Repositories;
using Meetup.ValueObjects;

namespace Meetup.Domain.Repositories;

public class AttendeeRepository : IAttendeeRepository
{
    // Временные заглушки (потом заменим на реальную БД)
    private readonly List<Attendee> _attendees = new();

    public Task<Attendee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = _attendees.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Attendee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Attendee>>(_attendees.ToList());
    }

    public Task AddAsync(Attendee entity, CancellationToken cancellationToken = default)
    {
        _attendees.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Attendee entity, CancellationToken cancellationToken = default)
    {
        // Для списка в памяти не нужно
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Attendee entity, CancellationToken cancellationToken = default)
    {
        _attendees.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exists = _attendees.Any(a => a.Id == id);
        return Task.FromResult(exists);
    }

    public Task<Attendee?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
    {
        var result = _attendees.FirstOrDefault(a => a.Username == username);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Registration>> GetAttendeeRegistrationsAsync(Guid attendeeId, CancellationToken cancellationToken = default)
    {
        // Пока возвращаем пустой список (потом свяжем с Registration)
        return Task.FromResult<IReadOnlyList<Registration>>(new List<Registration>());
    }
}