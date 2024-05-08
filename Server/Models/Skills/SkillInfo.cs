namespace Server.Models.Skills
{
    public class SkillInfo
    {
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
