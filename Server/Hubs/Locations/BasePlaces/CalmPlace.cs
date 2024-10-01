using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs.Locations.BasePlaces
{
    public abstract class CalmPlace : BasePlace
    {
        protected CalmPlace(IHubContext<PlaceHub> hubContext) : base(hubContext) {}
    }
}
