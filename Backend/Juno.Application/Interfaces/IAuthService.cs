using Juno.Application.DTOs.AuthDtos;
using Juno.Application.DTOs.UserDtos;

namespace Juno.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDetailsDto> Register(RegisterDto request);
        Task<LoginResponseDto> Login(LoginRequestDto request);
    }
}
