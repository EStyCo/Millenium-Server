using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class AttackMonsterDTO
    {
        public int IdMonster { get; set; }
        public int SkillId { get; set; }
        public string NameCharacter { get; set; } = string.Empty;
        public Place Place { get; set; }
    }
}
