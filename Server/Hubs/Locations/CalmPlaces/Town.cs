using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Hubs.Locations.BasePlaces;
using Server.Models.Entities;

namespace Server.Hubs.Locations.CalmPlaces
{
    public class Town : CalmPlace
    {
        public override string NamePlace { get; } = "town";
        public override Dictionary<string, ActiveUser> Users { get; protected set; } = new();

        public Town(IHubContext<PlaceHub> hubContext) : base(hubContext){}
    }
}
