using Juno.Domain.Enums.UserEnums;

namespace Juno.Application.DTOs.UserDtos
{
    public record UserDetailsDto(Guid Id, string Name, string Login, string Email, UserStatus Status, DateTime CreatedAt);
}
