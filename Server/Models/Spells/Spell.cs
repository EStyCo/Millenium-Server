using Server.Models.Utilities;

namespace Server.Models.Spells
{
    public abstract class Spell
    {
        public SpellType SpellType { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public int RestSeconds { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsReady { get; set; } = true;

        public abstract void Use(Entity user, params Entity[] targets);

        protected void SendBattleLog(string log, params Entity[] targets) 
        {
            foreach (var item in targets)
            { 
                var target = item as ActiveUser;
                if(target == null) continue;

                _ = target.AddBattleLog(log);
            }
        }
    }
}
