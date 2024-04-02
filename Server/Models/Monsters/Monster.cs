using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Server.Models.Monsters
{
    public abstract class Monster
    {
        public int Id { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public abstract int Attack();
    }
}
