using Microsoft.EntityFrameworkCore;
using Meetup.Domain;
using Meetup.Domain.Repositories.Abstractions;
using Meetup.Infrastructure.EntityFramework;
namespace Repositories.Implementations.EF;

public class EfEventRepository(ApplicationDbContext context)
    : EfRepository<Event, Guid>(context), IEventRepository
{
    private readonly DbSet<Event> _events = context.Set<Event>();

    public override async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _events
            .Include(e => e.Organizer)
            .Include(e => e.EventType)
            .Include("_registrations")
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    // Исправлено: Guid? вместо int?
    public async Task<IEnumerable<Event>> GetUpcomingEventsAsync(CancellationToken cancellationToken, Guid? eventTypeId = null)
    {
        var query = _events
            .Include(e => e.Organizer)
            .Include(e => e.EventType)
            .Where(e => e.IsActive && !e.IsCancelled && e.EventDate > DateTime.UtcNow);

        if (eventTypeId.HasValue)
            query = query.Where(e => e.EventType.Id == eventTypeId.Value);

        return await query.OrderBy(e => e.EventDate).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Event>> GetEventsByOrganizerAsync(Guid organizerId, CancellationToken cancellationToken)
        => await _events
            .Include(e => e.EventType)
            .Where(e => e.Organizer.Id == organizerId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Event>> GetEventsByTypeAsync(Guid eventTypeId, CancellationToken cancellationToken)
        => await _events
            .Include(e => e.Organizer)
            .Where(e => e.EventType.Id == eventTypeId && e.IsActive && !e.IsCancelled)
            .OrderBy(e => e.EventDate)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Event>> SearchEventsAsync(string? title, DateTime? fromDate, DateTime? toDate, string? location, CancellationToken cancellationToken)
    {
        var query = _events
            .Include(e => e.Organizer)
            .Include(e => e.EventType)
            .Where(e => e.IsActive && !e.IsCancelled);

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(e => e.Title.Value.Contains(title));

        if (fromDate.HasValue)
            query = query.Where(e => e.EventDate >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(e => e.EventDate <= toDate.Value);

        if (!string.IsNullOrWhiteSpace(location))
            query = query.Where(e => e.Location.Value.Contains(location));

        return await query.OrderBy(e => e.EventDate).ToListAsync(cancellationToken);
    }
}
