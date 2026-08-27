using Juno.Application.DTOs.OfficeDtos;
using Juno.Domain.Arguments.OfficeArguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.Mapper.OfficeMappers
{
    public static class OfficeDtoMapper
    {
        public static OfficeCreationData ToDto(this CreateOfficeDto data)
        {
            return new OfficeCreationData(data.Name, data.Email, data.DocumentNumber, data.PhoneNumber, data.LogoUrl, data.ZipCode, data.Street, data.AddressNumber, data.Neighborhood, data.City, data.State, data.Country);
        }
        
    }
}
