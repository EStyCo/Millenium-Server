namespace Server.Models.Skills
{
    public abstract class Skill
    {
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
    }
}
