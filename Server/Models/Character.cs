using Server.Models.Locations;
using Server.Models.Utilities;
using Area = Server.Models.Utilities.Area;

namespace Server.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string CharacterName { get; set; } = string.Empty;
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public Area CurrentArea { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public int TotalPoints { get; set; } = 0;
        public int FreePoints { get; set; } = 5;
        public int Strength { get; set; } = 5;
        public int Agility { get; set; } = 5;
        public int Intelligence { get; set; } = 5;
        public User User { get; set; }
    }
}
