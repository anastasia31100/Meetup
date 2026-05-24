using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class CompanyName(string? name) : ValueObject<string?>(new CompanyNameValidator(), name);