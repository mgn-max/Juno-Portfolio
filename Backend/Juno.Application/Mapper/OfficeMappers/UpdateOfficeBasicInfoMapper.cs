using Juno.Application.DTOs.OfficeDtos;
using Juno.Domain.Arguments.OfficeArguments;

namespace Juno.Application.Mapper.OfficeMappers
{
    public static class UpdateOfficeBasicInfoMapper
    {
        public static UpdateBasicInfoData ToDto(this UpdateBasicInfoDto data)
        {
            return new UpdateBasicInfoData(
                data.Name, 
                data.Email,
                data.DocumentNumber,
                data.PhoneNumber,
                data.LogoUrl
                ); 
        }
    }
}
