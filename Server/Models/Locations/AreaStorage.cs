using Server.Models.Utilities;

namespace Server.Models.Locations
{
    public class AreaStorage
    {
        private readonly Glade glade;
                    
        public AreaStorage(Glade _glade)
        { 
           glade = _glade;
        }

        public Area GetArea(Place place)
        {
            switch (place) 
            {
                case Place.Glade:
                    return glade;

                default:
                    return null;
            }    
        }
    }
}
