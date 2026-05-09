using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class EventTypeName(string name) : ValueObject<string>(new EventTypeNameValidator(), name);