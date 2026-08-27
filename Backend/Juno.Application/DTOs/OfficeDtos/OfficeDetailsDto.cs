using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Juno.Application.DTOs.OfficeDtos
{
    public record OfficeDetailsDto(Guid Id, string Name, string? Email, string? DocumentNumber, string? PhoneNumber, DateTime CreatedAt,string? ZipCode, string? Street, string? AddressNumber, string? Neighborhood, string? City, string? State, string? Country);
}
