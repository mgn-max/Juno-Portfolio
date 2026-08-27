using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Arguments.OfficeArguments
{
    public record ClientCreation(Guid UserId,Guid OfficeId, string Name, string? Email, string? PhoneNumber, string? DocumentNumber);
}

