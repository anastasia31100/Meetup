using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents company name (optional).
/// </summary>
public class CompanyName : ValueObject<string?>
{
    protected CompanyName() : base(new CompanyNameValidator(), default(string)) { }

    public CompanyName(string? name) : base(new CompanyNameValidator(), name) { }
}