using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.DTOs.OfficeDtos
{
    public record UpdateBasicInfoDto(string Name, string? Email, string? DocumentNumber, string? PhoneNumber, string? LogoUrl);
}
