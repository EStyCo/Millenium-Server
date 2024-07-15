namespace Server.Models.DTO.User
{
    public class StateDTO(string name, string description, string imagePath, int time)
    {
        public string Name { get; } = name;
        public string Description { get; } = description;
        public string ImagePath { get; } = imagePath;
        public int Time { get; } = time;
    }
}
