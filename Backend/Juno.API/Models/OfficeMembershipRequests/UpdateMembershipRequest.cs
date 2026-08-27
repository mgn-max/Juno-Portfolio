using Juno.Domain.Enums.OfficeMembershipEnums;

namespace Juno.API.Models.OfficeMembershipRequests
{
    public record UpdateMembershipRequest(MembershipProfile? Profile, MembershipStatus? Status, MembershipProfile RequestingProfile);
}
