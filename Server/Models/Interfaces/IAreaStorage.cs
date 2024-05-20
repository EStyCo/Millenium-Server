using Server.Hubs.Locations.BasePlaces;

namespace Server.Models.Interfaces
{
    public interface IAreaStorage
    {
        public BasePlace? GetPlace(string place);
        public BasePlace? GetPlaceById(string connectionId);
        public BattlePlace? GetBattlePlace(string place);
        public CalmPlace? GetCalmPlace(string place);
    }
}
