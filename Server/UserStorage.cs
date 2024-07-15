using Microsoft.AspNetCore.SignalR;
using Server.EntityFramework.Models;
using Server.Models.Inventory;
using Server.Models.Utilities;
using Server.Services;
using Server.Models;

namespace Server
{
    public class UserStorage : Hub
    {
        private readonly IHubContext<UserStorage> hubContext;
        private readonly PlaceService placeService;
        public Dictionary<string, CancellationTokenSource> DisconnectTokens { get; private set; }
        public List<ActiveUser> ActiveUsers { get; private set; }

        public UserStorage(IHubContext<UserStorage> _hubContext,
                           PlaceService _placeService)

        {
            hubContext = _hubContext;
            placeService = _placeService;
            ActiveUsers = [];
            DisconnectTokens = [];
        }

        public void ConnectHub(string name)
        {
            var activeUser = ActiveUsers.FirstOrDefault(x => x.Name == name);

            if (activeUser != null)
            {
                activeUser.ChangeConnectionId(Context.ConnectionId);
            }
        }

        public void LoginUser(Stats stats, Character character, List<Item> items)
        {
            var activeUser = ActiveUsers.FirstOrDefault(x => x.Name == character.Name);
            if (activeUser == null)
                AddNewUser(stats, character, items);
            else
                ReconnectUser(activeUser, character);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            var userToRemove = ActiveUsers.FirstOrDefault(user => user.ConnectionId == connectionId);
            if (userToRemove != null)
            {
                var cts = new CancellationTokenSource();
                DisconnectTokens.Add(connectionId, cts);

                _ = Task.Delay(TimeSpan.FromMinutes(1), cts.Token).ContinueWith(_ =>
                {
                    ActiveUsers.Remove(userToRemove);
                    DisconnectTokens.Remove(connectionId);
                }, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion); ;
            }

            await base.OnDisconnectedAsync(exception);
        }

        private void AddNewUser(Stats stats, Character character, List<Item> items)
        {
            ActiveUser newUser = new(hubContext, placeService, stats, character, items);
            newUser.ActiveSkills = SpellFactory.Get(character.Spells);
            ActiveUsers.Add(newUser);
        }

        private void ReconnectUser(ActiveUser activeUser, Character character)
        {
            if (DisconnectTokens.TryGetValue(activeUser.ConnectionId, out var cts))
            {
                cts.Cancel();
                DisconnectTokens.Remove(activeUser.ConnectionId);
            }
            activeUser.ActiveSkills = SpellFactory.Get(character.Spells);
        }
    }
}