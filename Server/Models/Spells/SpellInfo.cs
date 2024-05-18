namespace Server.Models.Skills
{
    public class SpellInfo
    {
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
