
using Meetup.Domain.Repositories.Abstractions.Base;

namespace Meetup.Domain.Repositories.Abstractions;

public interface IOrganizerRepository : IRepository<Organizer, Guid>
{
    Task<Organizer?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetOrganizerEventsAsync(Guid organizerId, CancellationToken cancellationToken, bool includeCancelled = false);
}
