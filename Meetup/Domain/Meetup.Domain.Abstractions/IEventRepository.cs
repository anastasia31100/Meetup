
using Meetup.Domain.Repositories.Abstractions.Base;

namespace Meetup.Domain.Repositories.Abstractions;

public interface IEventRepository : IRepository<Event, Guid>
{
    Task<IEnumerable<Event>> GetUpcomingEventsAsync(CancellationToken cancellationToken, Guid? eventTypeId = null);
    Task<IEnumerable<Event>> GetEventsByOrganizerAsync(Guid organizerId, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetEventsByTypeAsync(Guid eventTypeId, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> SearchEventsAsync(string? title, DateTime? fromDate, DateTime? toDate, string? location, CancellationToken cancellationToken);
}
