using Juno.Application.DTOs.OfficeDtos;
using Juno.Domain.Arguments.OfficeArguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.Mapper.OfficeMappers
{
    public static class UpdateOfficeAddressMapper
    {
        public static AddressUpdateData ToDto(this AddressUpdateDto dto)
        {
            return new AddressUpdateData(
                dto.ZipCode,
                dto.Street,
                dto.AddressNumber,
                dto.Neighborhood,
                dto.City,
                dto.State,
                dto.Country
            );
        }
    }
}
