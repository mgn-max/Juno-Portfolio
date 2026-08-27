namespace Juno.Application.DTOs.AuthDtos
{
    public record LoginResponseDto(Guid Id, string Name, string Email, string Token, DateTime ExpiresAt);
}
