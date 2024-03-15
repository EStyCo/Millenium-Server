using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Hubs;
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

        public List<ActiveUser> ActiveUsers = new List<ActiveUser>();

        public UserStorage(IHubContext<UserStorage> _hubContext, IServiceFactory<UserRepository> _userRepositoryFactory)
        {
            hubContext = _hubContext;
            userRepositoryFactory = _userRepositoryFactory;
        }

        public async Task ConnectHub(string name)
        {
            var userRepository = userRepositoryFactory.Create();
            var character = await userRepository.GetCharacter(name);

            if (character != null)
            {
                ActiveUser newUser = new(hubContext, character);
                newUser.ConnectionId = Context.ConnectionId;
                ActiveUsers.Add(newUser);

                await Task.WhenAll(newUser.StartVitalityConnection(), newUser.UpdateSpellList(character));
            }
        }

        public async Task BreakCharacter(string playerName)
        {
            var user = ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == playerName);

            if (user != null)
            {
                await Task.Delay(10);
                user.CurrentHP = new Random().Next(50, user.maxHP);
                user.CurrentMP = new Random().Next(50, user.maxMP);
            }
        }
    }
}
