using Juno.Domain.Enums.OfficeMembershipEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Projections.OfficeMembershipProjection
{
    public record OfficeMembershipWithDetails(Guid Id, Guid UserId, string UserName, Guid OfficeId, string OfficeName, MembershipProfile Profile, MembershipStatus Status, DateTime CreatedAt);
}
