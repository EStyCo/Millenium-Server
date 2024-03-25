using Client.MVVM.Model.Utilities;

namespace Client.MVVM.Model.DTO
{
    public class CharacterDTO
    {
        public string CharacterName { get; set; } = string.Empty;
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public Place CurrentArea { get; set; }
        public int Level { get; set; } = 1;
        public int CurrentEXP { get; set; } = 0;
        public int ToLevelExp { get; set; } = 0;
        public int TotalSpellPoints { get; set; } = 0;
        public int FreelSpellPoints { get; set; } = 0;
        public int TotalPoints { get; set; } = 0;
        public int FreePoints { get; set; } = 5;
        public int Strength { get; set; } = 5;
        public int Agility { get; set; } = 5;
        public int Intelligence { get; set; } = 5;
    }
}
