using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations;
using Server.Models;
using Server.Models.DTO;
using Server.Models.EntityFramework;
using Server.Models.Handlers;
using Server.Models.Interfaces;

namespace Server
{
    public class UserStorage : Hub
    {
        //private readonly IServiceFactory<UserRepository> userRepositoryFactory;
        private readonly IHubContext<UserStorage> hubContext;
        private readonly IAreaStorage areaStorage;
        public Dictionary<string, CancellationTokenSource> disconnectTokens = new();
        public List<ActiveUser> ActiveUsers { get; private set; }

        public UserStorage(IHubContext<UserStorage> _hubContext,
                           IAreaStorage _areaStorage)

        {
            hubContext = _hubContext;
            areaStorage = _areaStorage;
            ActiveUsers = new();
        }

        public void ConnectHub(string name)
        {
            var activeUser = ActiveUsers.FirstOrDefault(x => x.Name == name);

            if (activeUser != null)
            {
                activeUser.ChangeConnectionId(Context.ConnectionId);
            }
        }

        public void AddActiveUser(Stats stats, Character character)
        {
            var activeUser = ActiveUsers.FirstOrDefault(x => x.Name == character.Name);

            if (activeUser == null)
            {
                ActiveUser newUser = new(hubContext, areaStorage, stats, character);
                ActiveUsers.Add(newUser);
                newUser.CreateSpellList(character);
            }
            else
            {
                if (disconnectTokens.TryGetValue(activeUser.ConnectionId, out var cts))
                {
                    cts.Cancel();
                    disconnectTokens.Remove(activeUser.ConnectionId);
                }
                activeUser.CreateSpellList(character);
            }
        }

        /*        public async Task ChangeStats(UpdateStatDTO dto)
                {
                    var userRep = userRepositoryFactory.Create();

                    var stats = ActiveUsers
                        .Where(x => x.Name == dto.Name)
                        .Select(x => x.Stats)
                        .FirstOrDefault();

                    if (stats == null || !await userRep.UserExists(dto.Name)) return;

                    await userRep.UpdateStats(dto);

                    var newCounts = await userRep.GetStats(dto.Name);
                    if (newCounts != null)
                    {
                        stats.CreateStats(newCounts);
                    }
                }*/

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            var userToRemove = ActiveUsers.FirstOrDefault(user => user.ConnectionId == connectionId);
            if (userToRemove != null)
            {
                var cts = new CancellationTokenSource();
                disconnectTokens.Add(connectionId, cts);

                _ = Task.Delay(TimeSpan.FromMinutes(1), cts.Token).ContinueWith(_ =>
                {
                    ActiveUsers.Remove(userToRemove);
                    disconnectTokens.Remove(connectionId);
                }, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion);;
            }

            await base.OnDisconnectedAsync(exception);
        }

        public void AddExp(UpdateExpDTO dto)
        {
            var stats = ActiveUsers.Where(x => x.Name == dto.Name)
                       .Select(x => x.Stats)
                       .FirstOrDefault() as UserStatsHandler;

            stats?.AddExp(dto.Exp);
        }
    }
}