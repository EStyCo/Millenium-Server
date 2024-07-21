using Server.Models.Utilities.Slots;

namespace Server.EntityFramework.Models
{
    public class ItemEF
    {
        public int Id { get; set; }
        public ItemType Type { get; set; }
        public bool IsEquipped { get; set; } = false;
        public int CharacterId { get; set; }
        public CharacterEF Character { get; set; }
    }
}
