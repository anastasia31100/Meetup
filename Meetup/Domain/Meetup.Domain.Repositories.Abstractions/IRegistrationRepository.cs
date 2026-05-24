
using Meetup.Domain.Repositories.Abstractions.Base;

namespace Meetup.Domain.Repositories.Abstractions;

public interface IRegistrationRepository : IRepository<Registration, Guid>
{
    Task<Registration?> GetByEventAndAttendeeAsync(Guid eventId, Guid attendeeId, CancellationToken cancellationToken);
    Task<IEnumerable<Registration>> GetRegistrationsByEventAsync(Guid eventId, CancellationToken cancellationToken);
    Task<IEnumerable<Registration>> GetRegistrationsByAttendeeAsync(Guid attendeeId, CancellationToken cancellationToken);
    Task<int> GetActiveRegistrationsCountForEventAsync(Guid eventId, CancellationToken cancellationToken);
}
