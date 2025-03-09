namespace Server.Models.DTO.Auth
{
    public record EmailLoginRequest(string Email, string Password);
    public record GoogleLoginRequest(string UId);
}
