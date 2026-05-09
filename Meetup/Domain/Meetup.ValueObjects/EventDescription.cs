using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class EventDescription(string? description) : ValueObject<string?>(new DescriptionValidator(), description);