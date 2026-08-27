namespace Juno.API.Models.OfficeRequests
{
    public record CreateOfficeRequest(string Name, string? Email, string? DocumentNumber, string? PhoneNumber, string? LogoUrl, string? ZipCode, string? Street, string? AddressNumber, string? Neighborhood, string? City, string? State, string? Country);
}
