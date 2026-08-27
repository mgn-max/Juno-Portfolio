namespace Juno.API.Models.OfficeRequests
{
    public record UpdateOfficeBasicInfoRequest(string Name, string? Email, string? DocumentNumber, string? PhoneNumber, string? LogoUrl);
}
