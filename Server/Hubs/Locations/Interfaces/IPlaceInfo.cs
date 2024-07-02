namespace Server.Hubs.Locations.Interfaces
{
    public interface IPlaceInfo
    {
        public string ImagePath { get; }
        public string Description { get; }
        public string[] Routes { get; }
        public bool CanAttackUser { get; }
    }
}
