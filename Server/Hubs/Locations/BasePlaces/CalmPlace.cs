using Microsoft.AspNetCore.SignalR;
using Server.Models.Monsters;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class CalmPlace : BasePlace
    {
        protected CalmPlace(IHubContext<PlaceHub> hubContext) : base(hubContext) {}
    }
}
