namespace Server.Models.Modifiers.Additional
{
    public class AddRegeratedHP : Modifier
    {
        public override string Name { get; } = "Аура лечения восстанавливает дополнительное здоровье.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; }
        public override string Description { get; } = "";
    }
}
