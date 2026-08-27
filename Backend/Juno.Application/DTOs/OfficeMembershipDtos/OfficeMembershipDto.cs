using Juno.Domain.Enums.OfficeMembershipEnums;

namespace Juno.Application.DTOs.OfficeMembershipDtos
{
    public record OfficeMembershipDto(Guid Id, Guid UserId, string UserName, Guid OfficeId, string OfficeName, MembershipProfile Profile, MembershipStatus Status);
}
