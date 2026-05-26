
using Meetup.Domain.Repositories.Abstractions.Base;

namespace Meetup.Domain.Repositories.Abstractions;

public interface IAttendeeRepository : IRepository<Attendee, Guid>
{
    Task<Attendee?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<IEnumerable<Registration>> GetAttendeeActiveRegistrationsAsync(Guid attendeeId, CancellationToken cancellationToken);
    Task<IEnumerable<Registration>> GetAttendeeHistoryAsync(Guid attendeeId, CancellationToken cancellationToken);
}
