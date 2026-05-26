using Microsoft.EntityFrameworkCore;
using Meetup.Domain;
using Meetup.Domain.Repositories.Abstractions;
using Meetup.Infrastructure.EntityFramework;

namespace Repositories.Implementations.EF;

public class EfAttendeeRepository(ApplicationDbContext context)
    : EfRepository<Attendee, Guid>(context), IAttendeeRepository
{
    private readonly DbSet<Attendee> _attendees = context.Set<Attendee>();

    public override async Task<Attendee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _attendees
            .Include("_registrations")
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<Attendee?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        => await _attendees
            .Include("_registrations")
            .FirstOrDefaultAsync(a => a.Username.Value == username, cancellationToken);

    public async Task<IEnumerable<Registration>> GetAttendeeActiveRegistrationsAsync(Guid attendeeId, CancellationToken cancellationToken)
    {
        var attendee = await GetByIdAsync(attendeeId, cancellationToken);
        return attendee?.ActiveRegistrations ?? Enumerable.Empty<Registration>();
    }

    public async Task<IEnumerable<Registration>> GetAttendeeHistoryAsync(Guid attendeeId, CancellationToken cancellationToken)
    {
        var attendee = await GetByIdAsync(attendeeId, cancellationToken);
        return attendee?.History ?? Enumerable.Empty<Registration>();
    }
}
