using Server.Models.DTO.User;

namespace Server.Models.Monsters.DTO
{
    public class DetailsMonsterDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MaxHP { get; set; }
        public int Exp { get; set; } = 0;
        public string ImagePath { get; set; } = string.Empty;
        public int MinTimeAttack { get; set; }
        public int MaxTimeAttack { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public List<StateDTO> States { get; set; } = new();
    }
}
