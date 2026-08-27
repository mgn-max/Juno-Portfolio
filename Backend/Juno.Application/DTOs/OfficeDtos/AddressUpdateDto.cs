using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.DTOs.OfficeDtos
{
    public record AddressUpdateDto(string? ZipCode, string? Street, string? AddressNumber, string? Neighborhood, string? City, string? State, string? Country);
}
