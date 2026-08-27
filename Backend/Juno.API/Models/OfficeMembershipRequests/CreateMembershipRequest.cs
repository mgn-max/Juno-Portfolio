using Juno.Domain.Enums.OfficeMembershipEnums;

namespace Juno.API.Models.OfficeMembershipRequests
{
    public record CreateMembershipRequest(Guid UserId, Guid OfficeId, MembershipProfile Profile);
}
