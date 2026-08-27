using Juno.Application.DTOs.OfficeMembershipDtos;
using Juno.Application.Interfaces;
using Juno.Domain.Entities;
using Juno.Domain.Enums.OfficeMembershipEnums;
using Juno.Domain.Exceptions;
using Juno.Domain.Interfaces;
using Juno.Domain.Projections.OfficeMembershipProjection;

namespace Juno.Application.Services
{
    public class OfficeMembershipService : IOfficeMembershipService
    {
        private readonly IOfficeMembershipRepository _officeMembershipRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserRepository _userRepository;

        public OfficeMembershipService(IOfficeMembershipRepository officeMembershipRepository, IOfficeRepository officeRepository, IUserRepository userRepository)
        {
            _officeMembershipRepository = officeMembershipRepository;
            _officeRepository = officeRepository;
            _userRepository = userRepository;
        }

        public async Task<OfficeMembershipDetailsDto> CreateMembership(Guid userId, Guid officeId, MembershipProfile profile, MembershipProfile requestingProfile)
        {
            if (requestingProfile != MembershipProfile.Partner && requestingProfile != MembershipProfile.Admin)
                throw new BusinessRuleException("Apenas administradores e sócios podem criar associações de membros.");

            if (profile == MembershipProfile.Partner && requestingProfile != MembershipProfile.Partner)
                throw new BusinessRuleException("Apenas sócios podem atribuir o perfil de sócio.");

            if (await _officeMembershipRepository.ExistsByUserAndOffice(userId, officeId))
                throw new BusinessRuleException("Usuário já possui uma associação com este escritório.");

            var related = await GetUserAndOfficeNames(userId, officeId);

            var membership = new OfficeMembership(userId, officeId, profile);

            await _officeMembershipRepository.Add(membership);
            await _officeMembershipRepository.SaveChangesAsync();

            return MapToDetailsDto(membership, related.userName, related.officeName);
        }

        private async Task<OfficeMembership> GetSupportMembershipById(Guid id)
        {
            var membership = await _officeMembershipRepository.GetById(id);
            if (membership == null)
                throw new BusinessRuleException("Associação não encontrada");
            return membership;
        }

        public async Task<OfficeMembershipDto> GetMembershipById(Guid id)
        {
            var membership = await GetSupportMembershipById(id);

            var related = await GetUserAndOfficeNames(membership.UserId, membership.OfficeId);
            return MapToDto(membership, related.userName, related.officeName);
        }

        public async Task<OfficeMembershipDto> GetMembershipByUserAndOffice(Guid userId, Guid officeId)
        {
            var membership = await _officeMembershipRepository.GetByUserAndOffice(userId, officeId);
            if (membership == null)
                throw new BusinessRuleException("Associação não encontrada");

            var related = await GetUserAndOfficeNames(membership.UserId, membership.OfficeId);

            return MapToDto(membership, related.userName, related.officeName);
        }

        public async Task<OfficeMembershipDetailsDto> GetMembershipDetailsById(Guid id)
        {
            var membership = await GetSupportMembershipById(id);
            var related = await GetUserAndOfficeNames(membership.UserId, membership.OfficeId);

            return MapToDetailsDto(membership, related.userName, related.officeName);
        }

        public async Task<List<OfficeMembershipDto>> GetMembershipsByOfficeId(Guid officeId)
        {
            var memberships = await _officeMembershipRepository.GetByOfficeIdWithDetails(officeId);
            return memberships.Select(m => MapFromProjection(m)).ToList();
        }

        public async Task<List<OfficeMembershipDto>> GetMembershipsByUserId(Guid userId)
        {
            var memberships = await _officeMembershipRepository.GetByUserIdWithDetails(userId);
            return memberships.Select(m => MapFromProjection(m)).ToList();
        }

        public async Task UpdateMembership(Guid id, MembershipProfile? profile, MembershipStatus? status, MembershipProfile requestingProfile)
        {
            var membership = await GetSupportMembershipById(id);

            if (!status.HasValue && !profile.HasValue)
                throw new BusinessRuleException("Nenhuma alteração foi feita na associação.");

            var isRemovingPartnerProfile =
                membership.Profile == MembershipProfile.Partner &&
                profile.HasValue &&
                profile.Value != MembershipProfile.Partner;

            var isDisablingPartner =
                membership.Profile == MembershipProfile.Partner &&
                status.HasValue &&
                status.Value != MembershipStatus.Active;

            if (isRemovingPartnerProfile || isDisablingPartner)
            {
                var partnersCount = await _officeMembershipRepository.CountPartnersByOfficeId(membership.OfficeId);

                if (partnersCount <= 1)
                    throw new BusinessRuleException("Não é possível remover ou desativar o último sócio do escritório.");
            }

            if (profile.HasValue)
                membership.UpdateMembershipProfile(profile.Value, requestingProfile);

            if (status.HasValue)
                membership.UpdateMembershipStatus(status.Value, requestingProfile);

            await _officeMembershipRepository.SaveChangesAsync();
        }

        private async Task<(string userName, string officeName)> GetUserAndOfficeNames(Guid userId, Guid officeId)
        {
            var officeTask = _officeRepository.GetById(officeId);
            var userTask = _userRepository.GetById(userId);
            await Task.WhenAll(officeTask, userTask);
            var office = await officeTask;
            var user = await userTask;
            if (office == null)
                throw new BusinessRuleException("Escritório não encontrado");
            if (user == null)
                throw new BusinessRuleException("Usuário não encontrado");
            return (user.Name, office.Name);
        }

        private OfficeMembershipDto MapToDto(OfficeMembership membership, string userName, string officeName)
        {
            return new OfficeMembershipDto
            (
                membership.Id,
                membership.UserId,
                userName,
                membership.OfficeId,
                officeName,
                membership.Profile,
                membership.Status
            );
        }

        private OfficeMembershipDetailsDto MapToDetailsDto(OfficeMembership membership, string userName, string officeName)
        {
            return new OfficeMembershipDetailsDto
            (
                membership.Id,
                membership.UserId,
                userName,
                membership.OfficeId,
                officeName,
                membership.Profile,
                membership.Status,
                membership.CreatedAt
            );
        }

        private OfficeMembershipDto MapFromProjection(OfficeMembershipWithDetails membership)
        {
            return new OfficeMembershipDto
            (
                membership.Id,
                membership.UserId,
                membership.UserName,
                membership.OfficeId,
                membership.OfficeName,
                membership.Profile,
                membership.Status
            );
        }
    }
}
