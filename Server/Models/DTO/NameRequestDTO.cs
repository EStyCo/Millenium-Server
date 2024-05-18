namespace Server.Models.DTO
{
    public class NameRequestDTO
    {
        public NameRequestDTO(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
