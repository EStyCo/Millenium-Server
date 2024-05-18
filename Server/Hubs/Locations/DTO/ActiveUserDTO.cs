namespace Server.Hubs.Locations.DTO
{
    public class ActiveUserOnPlace
    {
        public string Name { get; } = string.Empty;
        public int Level { get; } = 0;

        public ActiveUserOnPlace(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }
}
