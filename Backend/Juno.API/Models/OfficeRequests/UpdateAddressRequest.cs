namespace Juno.API.Models.OfficeRequests
{
    public record UpdateAddressRequest(string? ZipCode, string? Street, string? AddressNumber, string? Neighborhood, string? City, string? State, string? Country);
}
