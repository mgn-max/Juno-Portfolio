using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Arguments.OfficeArguments
{
    public record UpdateBasicInfoData(string Name, string? Email, string? DocumentNumber, string? PhoneNumber, string? LogoUrl);
}
