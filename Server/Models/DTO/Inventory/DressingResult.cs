namespace Server.Models.DTO.Inventory
{
    public class DressingResult
    {
        public int EquippedId { get; }
        public int? UnEquippedId { get; }

        public DressingResult(int equippedId, int? unEquippedId)
        {
            EquippedId = equippedId;
            UnEquippedId = unEquippedId;
        }
    }
}
