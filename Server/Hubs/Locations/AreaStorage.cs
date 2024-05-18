using Server.Hubs.Locations.BasePlaces;

namespace Server.Hubs.Locations
{
    public class AreaStorage
    {
        private readonly IServiceProvider services;

        public AreaStorage( IServiceProvider _services)
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
                   .FirstOrDefault(place => place.ActiveUsers.ContainsKey(connectionId));
        }

        /*public BasePlace GetCalmPlace(string place)
        {
            switch (place)
            {
                case "glade": return glade;
                //case Place.DarkWood: return darkWood;

                default: return glade;
            }
        }*/

        public BattlePlace GetBattlePlace(string place)
        {
            var instance = services.GetServices<BattlePlace>().FirstOrDefault(x=> x.NamePlace == place);

            if (instance != null)
            { 
                return instance;
            }

            return null;
            /*var types = Assembly.GetExecutingAssembly().GetTypes();
             var matchingType = types
                .Where(x => x.IsSubclassOf(typeof(BasePlace)) && !x.IsAbstract)
                .FirstOrDefault(x => (string)x.GetProperty("NamePlace").GetValue(Activator.CreateInstance(x)) == place);*/



            Console.WriteLine('1');

            /*if (matchingType != null)
            {
                return services.GetService(matchingType) as BattlePlace;
            }*/


            /*switch (place)
            {
                case "glade": return glade;
                //case Place.DarkWood: return darkWood;

                default: return glade;
            }*/
        }
    }
}
