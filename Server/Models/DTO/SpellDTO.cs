using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class SpellDTO
    {
        public SpellType SpellType { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public int RestSeconds { get; set; }
        public string Description {  get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsReady { get; set; } = false;
    }
}