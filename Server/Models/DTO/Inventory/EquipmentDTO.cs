namespace Server.Models.DTO.Inventory
{
    public class EquipmentDTO
    {
        public ItemDTO Head { get; }
        public ItemDTO Body { get; }
        public ItemDTO Belt { get; }
        public ItemDTO Feet { get; }
        public ItemDTO Weapon { get; }

        public EquipmentDTO(ItemDTO head, ItemDTO body, ItemDTO belt, ItemDTO feet, ItemDTO weapon)
        {
            Head = head;
            Body = body;
            Belt = belt;
            Feet = feet;
            Weapon = weapon;
        }
    }
}
