using Juno.Domain.Entities;
using Juno.Domain.Projections.OfficeMembershipProjection;
namespace Juno.Domain.Interfaces
{
    public interface IOfficeMembershipRepository
    {
        Task Add(OfficeMembership membership);
        Task<OfficeMembership?> GetById(Guid id);
        Task<OfficeMembership?> GetByUserAndOffice(Guid userId, Guid officeId);
        Task<List<OfficeMembership>> GetByOfficeId(Guid officeId);
        Task<List<OfficeMembership>> GetByUserId(Guid userId);
        Task<List<OfficeMembershipWithDetails>> GetByOfficeIdWithDetails(Guid officeId);
        Task<List<OfficeMembershipWithDetails>> GetByUserIdWithDetails(Guid userId);
        Task<bool> ExistsByUserAndOffice(Guid userId, Guid officeId);
        Task<int> CountPartnersByOfficeId(Guid officeId);
        Task SaveChangesAsync();
    }
}
