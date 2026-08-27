using Juno.Domain.Entities;

namespace Juno.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
