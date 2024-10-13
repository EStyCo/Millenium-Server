using Server.Models.DTO.Chat;

namespace Server.Hubs
{
    public class ChatStorage
    {
        public List<Message> Messages { get; set; } = [];

        public Message AddMessage(string str, string name)
        {
            if (Messages.Count > 10) Messages.RemoveAt(0);
            Messages.Add(new()
            {
                Time = $"{DateTime.Now.ToString("HH:mm:ss")}",
                Name = name,
                Data = str
            });
            return Messages.Last();
        }
    }
}
