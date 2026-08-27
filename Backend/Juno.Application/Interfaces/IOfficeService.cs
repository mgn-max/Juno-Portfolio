using Juno.Application.DTOs.OfficeDtos;
using Juno.Domain.Arguments.OfficeArguments;

namespace Juno.Application.Interfaces
{
    public interface IOfficeService
    {
        #region [Office Management]
        Task<OfficeDetailsDto> CreateOffice(Guid currentUserId, CreateOfficeDto request);
        #endregion

        #region[Getter]
        Task<OfficeDto> GetOfficeById(Guid id);
        Task<OfficeDetailsDto> GetOfficeDetailsById(Guid id);
        #endregion

        #region[Updater]
        Task UpdateBasicInfo(Guid id, UpdateBasicInfoDto data);
        Task UpdateAddress(Guid id, AddressUpdateDto data);
        #endregion
    }
}
