using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Models;
using Server.Models.Interfaces;
using Server.Models.Utilities;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BasePlace
    {
        protected IHubContext<PlaceHub> HubContext { get; }
        public abstract string NamePlace { get; }
        public virtual bool CanAttackUser { get; } = false;
        public abstract Dictionary<string, ActiveUser> Users { get; protected set; }

        protected BasePlace(IHubContext<PlaceHub> hubContext)
        {
            HubContext = hubContext;
        }

        public virtual void AttackUser(ActiveUser user, ActiveUser target, SpellType type) 
        {
            if (user != null && target != null)
            { 
                user.UseSpell(type, target);
            }
        }

        public virtual async Task EnterPlace(ActiveUser user, string connectionId)
        {
            if (user == null) return;

            Users.Add(connectionId, user);

            if (HubContext.Clients != null) 
            { 
                await HubContext.Clients.Clients(Users.Keys).SendAsync("UpdateListUsers", Users.Values.Select(x => x.ToJson()).ToList());
            }
        }

        public virtual async Task LeavePlace(string connectionId)
        {
            var user = Users.FirstOrDefault(x => x.Key == connectionId);

            if (user.Value == null) return;

            Users.Remove(user.Key);

            if (HubContext.Clients != null)
                await HubContext.Clients.Clients(Users.Keys).SendAsync("UpdateListUsers", Users.Values.Select(x => x.ToJson()).ToList());
        }
    }
}