using Server.Models.Locations;

namespace Server.Models.DTO
{
    public class AttackMonsterDTO
    {
        public int IdMonster {  get; set; }
        public string NameCharacter { get; set; } = string.Empty;
        public string SkillNaming { get; set; } = string.Empty;
    }
}
