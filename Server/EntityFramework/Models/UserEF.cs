namespace Server.EntityFramework.Models
{
    public class UserEF
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public CharacterEF Character { get; set; }
    }
}
