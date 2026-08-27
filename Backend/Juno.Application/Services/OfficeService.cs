using Juno.Application.DTOs.OfficeDtos;
using Juno.Application.Exceptions;
using Juno.Application.Interfaces;
using Juno.Application.Mapper.OfficeMappers;
using Juno.Domain.Entities;
using Juno.Domain.Enums.OfficeMembershipEnums;
using Juno.Domain.Exceptions;
using Juno.Domain.Interfaces;

namespace Juno.Application.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IOfficeMembershipRepository _officeMembershipRepository;

        public OfficeService(IOfficeRepository officeRepository, IOfficeMembershipRepository officeMembershipRepository)
        {
            _officeRepository = officeRepository;
            _officeMembershipRepository = officeMembershipRepository;
        }

        public async Task UpdateAddress(Guid id, AddressUpdateDto data)
        {
            var office = await GetSupportOfficeById(id);
            office.UpdateAddress(data.ToDto());
            await _officeRepository.SaveChangesAsync();
        }

        public async Task<OfficeDetailsDto> CreateOffice(Guid currentUserId, CreateOfficeDto request)
        {
            var documentNumber = string.IsNullOrWhiteSpace(request.DocumentNumber) ? null : new string(request.DocumentNumber.Where(char.IsDigit).ToArray());
            var email = request.Email?.Trim();

            if (!string.IsNullOrWhiteSpace(email) && await _officeRepository.ExistsByEmail(email))
                throw new BusinessRuleException("Não é possivel cadastrar esse email pois ele já está em uso");
            if (!string.IsNullOrWhiteSpace(documentNumber) && await _officeRepository.ExistsByDocumentNumber(documentNumber))
                throw new BusinessRuleException("Não é possivel cadastrar esse número de documento pois ele já está em uso");

            var office = new Office(request.ToDto());

            await _officeRepository.Add(office);

            var membership = new OfficeMembership(
                currentUserId,
                office.Id,
                MembershipProfile.Partner
            );

            await _officeMembershipRepository.Add(membership);

            await _officeRepository.SaveChangesAsync();

            return ToDetailsDto(office);
        }

        private async Task<Office> GetSupportOfficeById(Guid id)
        {
            var office = await _officeRepository.GetById(id);
            if (office == null)
                throw new NotFoundException("Escritório não encontrado");
            return office;
        }

        public async Task<OfficeDto> GetOfficeById(Guid id)
        {
            var office = await GetSupportOfficeById(id);
            return new OfficeDto(office.Id, office.Name);
        }

        public async Task<OfficeDetailsDto> GetOfficeDetailsById(Guid id)
        {
            var office = await GetSupportOfficeById(id);
            return ToDetailsDto(office);
        }

        public async Task UpdateBasicInfo(Guid id, UpdateBasicInfoDto info)
        {
            var office = await GetSupportOfficeById(id);

            var email = info.Email?.Trim();
            var documentNumber = string.IsNullOrWhiteSpace(info.DocumentNumber) ? null : new string(info.DocumentNumber.Where(char.IsDigit).ToArray());

            if (!string.IsNullOrWhiteSpace(email) && await _officeRepository.ExistsByEmailExceptId(email, id))
                throw new BusinessRuleException("Não é possivel atualizar para esse email pois ele já está em uso");
            if (!string.IsNullOrWhiteSpace(documentNumber) && await _officeRepository.ExistsByDocumentNumberExceptId(documentNumber, id))
                throw new BusinessRuleException("Não é possivel atualizar para esse número de documento pois ele já está em uso");

            office.UpdateBasicInfo(info.ToDto());
            await _officeRepository.SaveChangesAsync();
        }

        private OfficeDetailsDto ToDetailsDto(Office office)
        {
            return new OfficeDetailsDto(
                office.Id,
                office.Name,
                office.Email,
                office.DocumentNumber,
                office.PhoneNumber,
                office.CreatedAt,
                office.ZipCode,
                office.Street,
                office.AddressNumber,
                office.Neighborhood,
                office.City,
                office.State,
                office.Country
            );
        }
    }
}
