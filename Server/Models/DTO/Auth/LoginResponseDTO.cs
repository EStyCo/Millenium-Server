namespace Server.Models.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}