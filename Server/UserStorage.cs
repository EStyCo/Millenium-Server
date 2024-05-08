using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Interfaces;
using Server.Repository;

namespace Server
{
    public class UserStorage : Hub
    {
        private readonly IServiceFactory<UserRepository> userRepositoryFactory;
        private readonly IHubContext<UserStorage> hubContext;
        public Dictionary<string, CancellationTokenSource> disconnectTokens = new();
        public List<ActiveUser> ActiveUsers { get; private set; }

        public UserStorage(IHubContext<UserStorage> _hubContext, IServiceFactory<UserRepository> _userRepositoryFactory)
        {
            hubContext = _hubContext;
            userRepositoryFactory = _userRepositoryFactory;
            ActiveUsers = new();
        }

        public async Task ConnectHub(string name)
        {
            var userRepository = userRepositoryFactory.Create();
            var character = await userRepository.GetCharacter(name);

            if (character == null) return;

            var activeUser = ActiveUsers.FirstOrDefault(x => x.Character.Name == name);

            if (activeUser == null)
            {
                ActiveUser newUser = new(hubContext, character);
                newUser.ConnectionId = Context.ConnectionId;
                ActiveUsers.Add(newUser);

                _ = newUser.StartVitalityConnection();
                _ = newUser.UpdateSpellList(character);
            }
            else 
            {
                if (disconnectTokens.TryGetValue(activeUser.ConnectionId, out var cts))
                { 
                    cts.Cancel();
                    disconnectTokens.Remove(activeUser.ConnectionId);
                }
                activeUser.ConnectionId = Context.ConnectionId;
                _ = activeUser.UpdateSpellList(activeUser.Character);
            }
        }

        public async Task<Character> UpdateStats(UpdateStatDTO dto)
        {
            var user = ActiveUsers.FirstOrDefault(x => x.Character.Name == dto.Name);
            var userRep = userRepositoryFactory.Create();

            if (user == null || !await userRep.UserExists(dto.Name)) return null;

            var character = await userRep.UpdateStats(dto);

            user.Character = character;
            return character;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            var userToRemove = ActiveUsers.FirstOrDefault(user => user.ConnectionId == connectionId);
            if (userToRemove != null)
            {
                var cts = new CancellationTokenSource();
                disconnectTokens.Add(connectionId, cts);

                Task.Delay(TimeSpan.FromMinutes(1), cts.Token).ContinueWith(_ =>
                {
                    ActiveUsers.Remove(userToRemove);
                    disconnectTokens.Remove(connectionId);
                }, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion); ;
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
