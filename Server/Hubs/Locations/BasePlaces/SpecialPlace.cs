using Microsoft.AspNetCore.SignalR;
using Server.Models.Utilities;
using Server.Models;
using Server.Hubs.Locations.Interfaces;
using Server.Models.Handlers.Vitality;

namespace Server.Hubs.Locations.BasePlaces
{
    public class SpecialPlace : BasePlace, IPlaceInfo, IBattleUsers
    {
        public override string NamePlace { get; } = "masturbation";
        public bool CanAttackUser { get; } = true;
        public override Dictionary<string, ActiveUser> Users { get; protected set; } = new();
        public string ImagePath { get; } = "masturbation.png";
        public string Description { get; } = "добро пожаловать в дрочильню";
        public string[] Routes { get; } = { "darkwood" };

        public SpecialPlace(IHubContext<PlaceHub> hubContext) : base(hubContext) { }

        public async void AttackUser(ActiveUser user, ActiveUser target, SpellType type)
        {
            var userOnPlace = Users.Values.FirstOrDefault(x => x.Name == user.Name);
            var targetOnPlace = Users.Values.FirstOrDefault(x => x.Name == target.Name);

            if (userOnPlace == null || targetOnPlace == null || !userOnPlace.CanAttack) return;

            userOnPlace.UseSpell(type, targetOnPlace);

            if (HubContext.Clients != null)
            {
                await HubContext.Clients.Clients(Users.Keys).SendAsync("UpdateListUsers", Users.Values.Select(x => x.ToJson()).ToList());
            }
        }

        public override void EnterPlace(ActiveUser user, string connectionId)
        {
            var handler = user.Vitality as UserVitalityHandler;
            if (handler != null)
                handler.OnVitalityChanged += UpdateListUsers;

            base.EnterPlace(user, connectionId);
        }

        public override void LeavePlace(string connectionId)
        {
            var user = Users.FirstOrDefault(x => x.Key == connectionId).Value;
            var handler = user.Vitality as UserVitalityHandler;
            if (handler != null)
                handler.OnVitalityChanged -= UpdateListUsers;

            base.LeavePlace(connectionId);
        }
    }
}