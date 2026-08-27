using Juno.Application.DTOs.UserDtos;
using Juno.Domain.Enums.UserEnums;

namespace Juno.Application.Interfaces
{
    public interface IUserService
    {
        #region [Getters]
        Task<UserDto> GetUserById(Guid id);
        Task<UserDetailsDto> GetUserDetailsById(Guid id);
        #endregion

        #region[Update Methods]
        Task Update(Guid id,string? name, string? login, string? email);
        Task UpdatePasswords(Guid id,string newPassword);
        Task UpdateStatus(Guid id,UserStatus status);
        Task UpdatePhotoUrl(Guid id,string? photoUrl);
        #endregion
    }
}
