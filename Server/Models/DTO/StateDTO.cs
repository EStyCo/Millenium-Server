namespace Server.Models.DTO
{
    public class StateDTO
    {
        public string Name { get; }
        public string Description { get; }
        public string ImagePath { get; }

        public StateDTO(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
        }
    }
}
