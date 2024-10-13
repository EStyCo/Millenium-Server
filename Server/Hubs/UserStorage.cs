using Microsoft.AspNetCore.SignalR;
using Server.Models.Entities;

namespace Server.Hubs
{
    public class UserStorage : Hub
    {
        public Dictionary<string, CancellationTokenSource> DisconnectTokens { get; private set; } = [];
        public List<ActiveUser> ActiveUsers { get; private set; } = [];

        public ActiveUser? GetUser(string name)
        {
            return ActiveUsers.FirstOrDefault(x => x.Name == name);
        }

        #region HubsMethods
        public void ConnectHub(string name)
        {
            var activeUser = ActiveUsers.FirstOrDefault(x => x.Name == name);
            if (activeUser != null)
                activeUser.ChangeConnectionId(Context.ConnectionId);
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
        #endregion
    }
}