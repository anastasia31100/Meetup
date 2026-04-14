using Meetup.Domain.Entities;
using Meetup.Domain.Abstractions.Repositories;
using Meetup.Domain.Entities;
using Meetup.ValueObjects;

namespace Meetup.Domain.Repositories;

public class OrganizerRepository : IOrganizerRepository
{
    private readonly List<Organizer> _organizers = new();
    private readonly List<Event> _events = new();

    public Task<Organizer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = _organizers.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Organizer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Organizer>>(_organizers.ToList());
    }

    public Task AddAsync(Organizer entity, CancellationToken cancellationToken = default)
    {
        _organizers.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Organizer entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Organizer entity, CancellationToken cancellationToken = default)
    {
        _organizers.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_organizers.Any(o => o.Id == id));
    }

    public Task<Organizer?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
    {
        var result = _organizers.FirstOrDefault(o => o.Username == username);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Event>> GetOrganizerEventsAsync(Guid organizerId, CancellationToken cancellationToken = default)
    {
        var result = _events.Where(e => e.OrganizerId == organizerId).ToList();
        return Task.FromResult<IReadOnlyList<Event>>(result);
    }
}