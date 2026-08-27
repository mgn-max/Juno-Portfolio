using Juno.API.Models.OfficeRequests;
using Juno.Application.DTOs.OfficeDtos;

namespace Juno.API.Mapper.OfficeMapper
{
    public static class CreateOfficeRequestMapper
    {
        public static CreateOfficeDto ToDto(this CreateOfficeRequest data)
        {
            return new CreateOfficeDto(
                data.Name, 
                data.Email,
                data.DocumentNumber, 
                data.PhoneNumber, 
                data.LogoUrl,
                data.ZipCode,
                data.Street,
                data.AddressNumber, 
                data.Neighborhood, 
                data.City, 
                data.State, 
                data.Country
                );
        }
    }
}
