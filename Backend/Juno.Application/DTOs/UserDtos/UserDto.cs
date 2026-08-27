using Juno.Domain.Enums.UserEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.DTOs.UserDtos
{
    public record UserDto(Guid Id, string Name, UserStatus Status);
}
