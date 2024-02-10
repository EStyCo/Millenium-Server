using Server.Models.Utilities;

namespace Server.Models
{
    public class ActivityUser
    {
        public string CharacterName { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public Area CurrentArea { get; set; }
    }
}
