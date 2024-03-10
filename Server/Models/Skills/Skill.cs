using Server.Models.DTO;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public abstract class Skill
    {
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public int Id { get; set; }
        public int RestSeconds { get; set; } = 0;
        public bool IsReady { get; set; } = true;

        protected SendRestTimeDelegate SendRestDelegate;

        public abstract Task<int> Attack(CharacterDTO c);
    }
}
