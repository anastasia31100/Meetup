using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class Username(string name) : ValueObject<string>(new UsernameValidator(), name);