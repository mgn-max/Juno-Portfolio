namespace Juno.API.Models.AuthRequests
{
    public record RegisterRequest(string Name, string Login, string Email, string Password, string? PhotoUrl);
}
