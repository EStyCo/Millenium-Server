using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Server.Models.Monsters
{
    public abstract class Monster : Entity
    {
        public int Id { get; set; }
        public int CurrentHP { get; set; } = 0;
        public int MaxHP { get; set; } = 0;
        public int Exp { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public abstract int Attack();
    }
}
