namespace Server.Models.DTO.User
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
