namespace Server.Hubs.Locations.Interfaces
{
    public interface IPlaceInfo
    {
        public string ImagePath { get; }
        public string Description { get; }
        public IRoute[] Routes { get; }
        public bool CanAttackUser { get; }
    }
}
