namespace Server.Models
{
    public class CharacterVitality
    {
        public string Name { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public bool isRegen { get; set; } = false;
    }
}
