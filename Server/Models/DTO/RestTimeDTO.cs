using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class RestTimeDTO
    {
        public SpellType SpellType { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RestSeconds { get; set; }
        public bool IsReady { get; set; } = false;
    }
}
