using Server.Hubs.Locations.BasePlaces;
using Server.Models.Interfaces;

namespace Server.Hubs.Locations
{
    public class AreaStorage : IAreaStorage
    {
        private readonly IServiceProvider services;

        public AreaStorage(IServiceProvider _services)
        {
            services = _services;
        }

        public BasePlace? GetPlace(string place)
        {
            return services.GetServices<BasePlace>()
                           .FirstOrDefault(x => x.NamePlace == place);
        }

        public BasePlace? GetPlaceById(string connectionId)
        {
            return services.GetServices<BasePlace>()
                           .FirstOrDefault(place => place.Users.ContainsKey(connectionId));
        }

        public BattlePlace? GetBattlePlace(string place)
        {
            return services.GetServices<BasePlace>()
                           .FirstOrDefault(x => x.NamePlace == place) as BattlePlace;
        }

        public CalmPlace? GetCalmPlace(string place)
        {
            return services.GetServices<BasePlace>()
                           .FirstOrDefault(x => x.NamePlace == place) as CalmPlace;
        }
    }
}