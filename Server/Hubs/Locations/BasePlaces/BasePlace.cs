﻿using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.DTO;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class BasePlace
    {
        public abstract string NamePlace { get; }
        public abstract Dictionary<string, ActiveUserOnPlace> ActiveUsers { get; protected set; }

        protected IHubContext<PlaceHub> HubContext { get; }

        protected BasePlace(IHubContext<PlaceHub> hubContext)
        {
            HubContext = hubContext;
        }

        public async Task EnterPlace(string name, int level, string connectionId)
        {
            var user = ActiveUsers.FirstOrDefault(x => x.Key == connectionId);

            if (name == null || user.Value != null) return;

            ActiveUsers.Add(connectionId, new(name, level));

            if (HubContext.Clients != null)
                await HubContext.Clients.All.SendAsync("UpdateListUsers", ActiveUsers);
        }

        public async Task LeavePlace(string connectionId)
        {
            var user = ActiveUsers.FirstOrDefault(x => x.Key == connectionId);

            if (user.Value == null) return;

            ActiveUsers.Remove(user.Key);

            if (HubContext.Clients != null)
                await HubContext.Clients.All.SendAsync("UpdateListUsers", ActiveUsers);
        }
    }
}