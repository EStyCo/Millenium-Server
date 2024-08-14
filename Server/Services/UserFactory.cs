using Server.EntityFramework.Models;
using Server.Models.Inventory;
using Server.Models.Utilities;
using Server.Models;
using Microsoft.AspNetCore.SignalR;
using Server.Models.Interfaces;

namespace Server.Services
{
    public class UserFactory
    {
        private readonly IHubContext<UserStorage> hubContext;
        private readonly UserStorage userStorage;

        public UserFactory(IHubContext<UserStorage> _hubContext,
                           UserStorage _userStorage)
        {
            hubContext = _hubContext;
            userStorage = _userStorage;
        }

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
            //activeUser.ActiveSkills = SpellFactory.Get(character.Spells);
        }
    }
}
