using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Arguments.OfficeArguments
{
    public record AddressUpdateData(string? ZipCode, string? Street, string? AddressNumber, string? Neighborhood, string? City, string? State, string? Country);
}
