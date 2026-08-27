using Juno.API.Models.OfficeRequests;
using Juno.Application.DTOs.OfficeDtos;

namespace Juno.API.Mapper.OfficeMapper
{
    public static class UpdateAddressMapper
    {
     public static AddressUpdateDto ToDto(this UpdateAddressRequest request)
        {
            return new AddressUpdateDto(request.ZipCode, 
                request.Street, 
                request.AddressNumber, 
                request.Neighborhood, 
                request.City,
                request.State, 
                request.Country
                );
        }
    }
}
