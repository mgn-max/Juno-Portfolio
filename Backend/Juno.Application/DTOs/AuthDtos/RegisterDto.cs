namespace Juno.Application.DTOs.AuthDtos
{
    public record RegisterDto(string Name, string Login, string Email, string Password, string? PhotoUrl);
}
