namespace Server.Models.DTO.User
{
    public class TravelDTO
    {
        public string Name { get; } = string.Empty;
        public string Place { get; } = string.Empty;

        public TravelDTO(string name, string place)
        {
            Name = name;
            Place = place;
        }
    }
}
