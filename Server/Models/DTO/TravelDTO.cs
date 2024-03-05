using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class TravelDTO
    {
        public string CharacterName { get; set; } = string.Empty;
        public Place Area { get; set; }
    }
}
