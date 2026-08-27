using Juno.Domain.Entities;

namespace Juno.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region[User Management]
        Task Add(User user);
        Task SaveChangesAsync();
        #endregion

        #region [Getters]
        Task<User?> GetById(Guid id);
        Task<User?> GetByLogin(string login);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByLoginOrEmail(string loginOrEmail);
        #endregion

        #region [Existence Checks]
        Task<bool> ExistsByLogin(string login);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByLoginExceptId(string login, Guid id);
        Task<bool> ExistsByEmailExceptId(string email, Guid id);
        #endregion

    }
}
