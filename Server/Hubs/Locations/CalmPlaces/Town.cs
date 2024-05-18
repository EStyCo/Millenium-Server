using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.BasePlaces;
using Server.Hubs.Locations.DTO;

namespace Server.Hubs.Locations.CalmPlaces
{
    public class Town : CalmPlace
    {
        public override string NamePlace { get; } = "town";
        public override Dictionary<string, ActiveUserOnPlace> ActiveUsers { get; protected set; } = new();

        public Town(IHubContext<PlaceHub> hubContext) : base(hubContext){}
    }
}
