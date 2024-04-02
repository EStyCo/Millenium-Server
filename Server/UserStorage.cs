using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Interfaces;
using Server.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Server
{
    public class UserStorage : Hub
    {
        private readonly IServiceFactory<UserRepository> userRepositoryFactory;
        private readonly IHubContext<UserStorage> hubContext;

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

            if (character != null)
            {
                ActiveUser newUser = new(hubContext, character);
                newUser.ConnectionId = Context.ConnectionId;
                ActiveUsers.Add(newUser);

                await Task.WhenAll(newUser.StartVitalityConnection(), newUser.UpdateSpellList(character));
            }
        }

        public async Task<CharacterDTO> UpdateStats(UpdateStatDTO dto)
        {
            var user = ActiveUsers.FirstOrDefault(x => x.Character.CharacterName == dto.Name);
            var userRep = userRepositoryFactory.Create();

            if (user == null || !await userRep.UserExists(dto.Name)) return null;

            var character = await userRep.UpdateStats(dto);

            user.Character = character;
            return character;
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
