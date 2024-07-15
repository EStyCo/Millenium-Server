namespace Server.Models.DTO.Inventory
{
    public class InventoryDTO(List<ItemDTO> items)
    {
        public List<ItemDTO> Items { get; set; } = items;
    }
}
