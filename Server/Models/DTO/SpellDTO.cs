namespace Server.Models.DTO
{
    public class SpellDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public string Description {  get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsReady { get; set; } = false;
    }
}
