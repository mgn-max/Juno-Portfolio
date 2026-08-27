using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.DTOs.AuthDtos
{
    public record LoginRequestDto(string LoginOrEmail, string Password);
}
