using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.DTOs.OfficeDtos
{
    public record CreateOfficeDto(string Name, string? Email, string? DocumentNumber, string? PhoneNumber,string? LogoUrl, string? ZipCode, string? Street, string? AddressNumber, string? Neighborhood, string? City, string? State, string? Country);
}
