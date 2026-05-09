using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.ValueObjects.Exceptions;

public class ValidatorNullException(string paramName)
    : ArgumentNullException(paramName, $"Для типа необходимо указать валидатор \"{paramName}\".");
