using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Hubs.DTO;
using Server.Models.DTO.Chat;
using Server.Models.DTO.User;
using Server.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Hubs
{
    public class ChatHub(
        UserStorage userStorage,
        ChatStorage chatStorage) : Hub
    {
        public async Task ConnectToHub(string name)
        {
            var user = userStorage.GetUser(name);
            if (user == null)
            {
                await OnDisconnectedAsync(new());
                return;
            }
        }

        public async void AddMessage(string messageDTO, string name)
        {
            var message = chatStorage.AddMessage(messageDTO, name);

            await Clients.All.SendAsync("UpdateChat", message);
        }

        public List<Message> LastMessages()
        {
            return chatStorage.Messages;
        }
    }
}
