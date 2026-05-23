using Microsoft.EntityFrameworkCore;
using Meetup.Domain;
using Meetup.Domain.Repositories.Abstractions;
using Meetup.Infrastructure.EntityFramework;
namespace Repositories.Implementations.EF;

public class EfRegistrationRepository(ApplicationDbContext context)
    : EfRepository<Registration, Guid>(context), IRegistrationRepository
{
    private readonly DbSet<Registration> _registrations = context.Set<Registration>();

    public override async Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _registrations
            .Include(r => r.Event)
            .Include(r => r.Attendee)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<Registration?> GetByEventAndAttendeeAsync(Guid eventId, Guid attendeeId, CancellationToken cancellationToken)
        => await _registrations
            .Include(r => r.Event)
            .Include(r => r.Attendee)
            .FirstOrDefaultAsync(r => r.Event.Id == eventId && r.Attendee.Id == attendeeId && !r.IsCancelled, cancellationToken);

    public async Task<IEnumerable<Registration>> GetRegistrationsByEventAsync(Guid eventId, CancellationToken cancellationToken)
        => await _registrations
            .Include(r => r.Attendee)
            .Where(r => r.Event.Id == eventId && !r.IsCancelled)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Registration>> GetRegistrationsByAttendeeAsync(Guid attendeeId, CancellationToken cancellationToken)
        => await _registrations
            .Include(r => r.Event)
            .Where(r => r.Attendee.Id == attendeeId)
            .OrderByDescending(r => r.RegisteredAt)
            .ToListAsync(cancellationToken);

    public async Task<int> GetActiveRegistrationsCountForEventAsync(Guid eventId, CancellationToken cancellationToken)
        => await _registrations
            .CountAsync(r => r.Event.Id == eventId && !r.IsCancelled, cancellationToken);
}