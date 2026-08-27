using Juno.API.Models.OfficeRequests;
using Juno.Application.DTOs.OfficeDtos;

namespace Juno.API.Mapper.OfficeMapper
{
    public static class UpdateBasicInfoMapper
    {
        public static UpdateBasicInfoDto ToDto(this UpdateOfficeBasicInfoRequest request)
        {
            return new UpdateBasicInfoDto(request.Name, request.Email, request.DocumentNumber, request.PhoneNumber, request.LogoUrl);
        }
    }
}
