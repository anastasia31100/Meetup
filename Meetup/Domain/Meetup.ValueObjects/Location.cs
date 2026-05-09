using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class Location(string location) : ValueObject<string>(new LocationValidator(), location);