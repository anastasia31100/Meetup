using Meetup.Domain.Entities;
using Meetup.Domain.Abstractions.Repositories;
using Meetup.Domain.Entities;
using Meetup.ValueObjects;

namespace Meetup.Domain.Repositories;

public class EventRepository : IEventRepository
{
    private readonly List<Event> _events = new();

    public Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = _events.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Event>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Event>>(_events.ToList());
    }

    public Task AddAsync(Event entity, CancellationToken cancellationToken = default)
    {
        _events.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Event entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Event entity, CancellationToken cancellationToken = default)
    {
        _events.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_events.Any(e => e.Id == id));
    }

    public Task<IReadOnlyList<Event>> GetUpcomingEventsAsync(CancellationToken cancellationToken = default)
    {
        var result = _events.Where(e => e.Date.Value > DateTime.Now && e.IsActive).ToList();
        return Task.FromResult<IReadOnlyList<Event>>(result);
    }

    public Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(Guid organizerId, CancellationToken cancellationToken = default)
    {
        var result = _events.Where(e => e.OrganizerId == organizerId).ToList();
        return Task.FromResult<IReadOnlyList<Event>>(result);
    }

    public Task<IReadOnlyList<Event>> GetEventsByTypeAsync(Guid eventTypeId, CancellationToken cancellationToken = default)
    {
        var result = _events.Where(e => e.EventTypeId == eventTypeId).ToList();
        return Task.FromResult<IReadOnlyList<Event>>(result);
    }

    public Task<IReadOnlyList<Event>> SearchByTitleAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        var result = _events.Where(e => e.Title.Value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        return Task.FromResult<IReadOnlyList<Event>>(result);
    }
}