namespace Server.Models.Inventory
{
    public class Inventory
    {
        public List<Item> Items { get; private set; } = [];

        public void AddItem(Item item) { }
        public void TakeItem(Item item) { }
        public void DeleteItem(Item item) { }
    }
}
