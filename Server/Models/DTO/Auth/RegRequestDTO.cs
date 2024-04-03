using Server.Models.Utilities;

namespace Server.Models.DTO.Auth
{
    public class RegRequestDTO
    {
        public string CharacterName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Race Race { get; set; }
    }
}
