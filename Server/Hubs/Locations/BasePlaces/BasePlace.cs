using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.Models.Handlers;
using Server.Models.Utilities;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BasePlace
    {
        protected IHubContext<PlaceHub> HubContext { get; }

        public abstract string NamePlace { get; }
        public abstract Dictionary<string, ActiveUser> Users { get; protected set; }

        protected BasePlace(IHubContext<PlaceHub> hubContext)
        {
            HubContext = hubContext;
        }

        public virtual void EnterPlace(ActiveUser user, string connectionId)
        {
            if (user == null) return;

            Users.Add(connectionId, user);
            UpdateListUsers();
        }

        public virtual void LeavePlace(string connectionId)
        {
            var user = Users.FirstOrDefault(x => x.Key == connectionId);

            if (user.Value == null) return;

            //(user.Value.Vitality as UserVitalityHandler)?.ChangePlace(null);

            Users.Remove(user.Key);
            UpdateListUsers();
        }

        public async void UpdateListUsers()
        {
            if (HubContext.Clients != null)
                await HubContext.Clients.Clients(Users.Keys).SendAsync("UpdateListUsers", Users.Values.Select(x => x.ToJson()).ToList());
        }
    }
}