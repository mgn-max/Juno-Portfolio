using Juno.API.Models.AuthRequests;
using Juno.Application.DTOs.AuthDtos;

namespace Juno.API.Mapper.AuthMapper
{
    public static class RegisterRequestMapper
    {
        public static RegisterDto ToDto(this RegisterRequest data)
        {
            return new RegisterDto
            (
                data.Name,
                data.Login,
                data.Email,
                data.Password,
                data.PhotoUrl
            );
        }
    }
}
