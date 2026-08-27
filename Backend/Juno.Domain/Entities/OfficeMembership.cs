using Juno.Domain.Enums.OfficeMembershipEnums;
using Juno.Domain.Exceptions;

namespace Juno.Domain.Entities
{
    public class OfficeMembership
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid OfficeId { get; private set; }

        public MembershipProfile Profile { get; private set; }
        public MembershipStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private OfficeMembership() { }

        public OfficeMembership(Guid userId, Guid officeId, MembershipProfile profile = MembershipProfile.Associate)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("O id do usuário é inválido");
            if (officeId == Guid.Empty)
                throw new ArgumentException("O id do escritorio é inválido");

            Id = Guid.NewGuid();
            UserId = userId;
            OfficeId = officeId;
            Profile = profile;
            Status = MembershipStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateMembershipStatus(MembershipStatus status, MembershipProfile requestingProfile)
        {
            if (requestingProfile != MembershipProfile.Admin && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas administradores ou sócios podem alterar o status do usuário");
            if (Profile == MembershipProfile.Partner && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas um sócio pode alterar o status de outro sócio");
            if (Status == status)
                throw new BusinessRuleException("O usuário já está com o status informado");

            Status = status;
        }
        public void UpdateMembershipProfile(MembershipProfile profile, MembershipProfile requestingProfile)
        {
            if (Profile == MembershipProfile.Partner && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas um sócio pode alterar o perfil de outro sócio");
            if (Profile == MembershipProfile.Admin && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas um sócio pode alterar o perfil de um administrador");
            if (profile == MembershipProfile.Partner && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas um sócio pode promover usuários a sócio");
            if (requestingProfile != MembershipProfile.Admin && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas administradores ou sócios podem alterar o perfil dos usuários");
            if (Profile == profile)
                throw new BusinessRuleException("O usuário já possui o perfil informado");

            Profile = profile;
        }

    }
}
