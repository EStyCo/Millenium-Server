using Server.Models.Handlers.Stats;
using Server.Models.Modifiers.Additional.Stats;

namespace Server.Models.DTO.User
{
    public class StatDTO(UserStatsHandler stats)
    {
        public int Level { get; } = stats.Level;
        public int CurrentExp { get; } = stats.CurrentExp;
        public int ToLevelExp { get; } = stats.ToLevelExp;
        public int FreePoints { get; } = stats.FreePoints;
        public int Strength { get; } = stats.Strength;
        public int AddStrength { get; } = stats.Entity?.Modifiers?.Get<AddStrength>()?.Value ?? 0;
        public int Agility { get; } = stats.Agility;
        public int AddAgility { get; } = stats.Entity?.Modifiers?.Get<AddAgility>()?.Value ?? 0;
        public int Vitality { get; } = stats.Vitality;
        public int AddVitality { get; } = stats.Entity?.Modifiers?.Get<AddVitality>()?.Value ?? 0;
        public int Intelligence { get; } = stats.Intelligence;
        public int AddIntelligence { get; } = stats.Entity?.Modifiers?.Get<AddIntelligence>()?.Value ?? 0;
        public int Mastery { get; } = stats.Mastery;
        public int AddMastery { get; } = stats.Entity?.Modifiers?.Get<AddMastery>()?.Value ?? 0;
        public int Luck { get; } = stats.Luck;
        public int AddLuck { get; } = stats.Entity?.Modifiers?.Get<AddLuck>()?.Value ?? 0;
    }
}
