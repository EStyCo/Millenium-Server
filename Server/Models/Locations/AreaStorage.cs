using Server.Models.Utilities;

namespace Server.Models.Locations
{
    public class AreaStorage
    {
        private readonly Glade glade;
        private readonly DarkWood darkWood;
                    
        public AreaStorage(Glade _glade,
                           DarkWood _darkWood)
        { 
            glade = _glade;
            darkWood = _darkWood;
        }

        public Area GetArea(Place place)
        {
            switch (place) 
            {
                case Place.Glade: return glade;
                case Place.DarkWood: return darkWood;

                default: return null;
            }    
        }
    }
}
