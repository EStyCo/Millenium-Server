using Server.Models.Utilities;

namespace Server.Models.EntityFramework
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
