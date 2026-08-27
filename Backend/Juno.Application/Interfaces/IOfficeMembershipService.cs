using Juno.Application.DTOs.OfficeMembershipDtos;
using Juno.Domain.Enums.OfficeMembershipEnums;
namespace Juno.Application.Interfaces
{
    public interface IOfficeMembershipService
    {
        Task<OfficeMembershipDetailsDto> CreateMembership(
           Guid userId,
           Guid firmId,
           MembershipProfile profile,
           MembershipProfile requestingProfile);
        Task<OfficeMembershipDto> GetMembershipById(Guid id);
        Task<OfficeMembershipDetailsDto> GetMembershipDetailsById(Guid id);
        Task<OfficeMembershipDto> GetMembershipByUserAndOffice(Guid userId, Guid officeId);
        Task<List<OfficeMembershipDto>> GetMembershipsByOfficeId(Guid officeId);
        Task<List<OfficeMembershipDto>> GetMembershipsByUserId(Guid userId);

        Task UpdateMembership(
            Guid id,
            MembershipProfile? profile,
            MembershipStatus? status,
            MembershipProfile requestingProfile);
    }
}
