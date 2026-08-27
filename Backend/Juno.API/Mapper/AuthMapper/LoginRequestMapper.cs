using Juno.API.Models.AuthRequests;
using Juno.Application.DTOs.AuthDtos;

namespace Juno.API.Mapper.AuthMapper
{
    public static class LoginRequestMapper
    {
        public static LoginRequestDto ToDto(this LoginRequest data)
        {
            return new LoginRequestDto(
                data.LoginOrEmail,
                data.Password
                );
        }
    }
}
