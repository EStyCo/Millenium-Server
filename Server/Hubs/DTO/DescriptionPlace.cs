namespace Server.Hubs.DTO
{
    public class DescriptionPlace(string imagePath, string description, bool canAttackUser)
    {
        public string ImagePath { get; } = imagePath;
        public string Description { get; } = description;
        public bool CanAttackUser { get; } = canAttackUser;
    }
}
