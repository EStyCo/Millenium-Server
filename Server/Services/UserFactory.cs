using Server.EntityFramework.Models;
using Microsoft.AspNetCore.SignalR;
using Server.Models.Entities;
using Server.Hubs;

namespace Server.Services
{
    public class UserFactory(
        IHubContext<UserStorage> hubContext,
        UserStorage userStorage)
    {
        public void LoginUser(CharacterEF character)
        {
            var activeUser = userStorage.GetUser(character.Name) as ActiveUser;
            if (activeUser != null)
                ReconnectUser(activeUser, character);
            else
                AddNewUser(character);
        }

        private void AddNewUser(CharacterEF character)
        {
            ActiveUser newUser = new();
            newUser.ChangeHubContext(hubContext);
            newUser.Initialize(character);
            newUser.ReAssembly();
            userStorage.ActiveUsers.Add(newUser);
        }

        private void ReconnectUser(ActiveUser activeUser, CharacterEF character)
        {
            if (userStorage.DisconnectTokens.TryGetValue(activeUser.ConnectionId, out var cts))
            {
                cts.Cancel();
                userStorage.DisconnectTokens.Remove(activeUser.ConnectionId);
            }
        }
    }
}
