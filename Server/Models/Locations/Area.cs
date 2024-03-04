using Server.Models.Monsters;
using Server.Models.Utilities;

namespace Server.Models.Locations
{
    public abstract class Area
    {
        public Utilities.Area CurrentArea {  get; set; }
        public List<Monster> Monsters { get; set; } = new();

        public abstract Task AddMonster();
        public abstract Task DeleteMonster(int id);
        public abstract Task<List<Monster>> GetMonster();
    }
}
