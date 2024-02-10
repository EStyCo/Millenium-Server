using Server.Models.Utilities;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public Area CurrentArea { get; set; }
    }
}
