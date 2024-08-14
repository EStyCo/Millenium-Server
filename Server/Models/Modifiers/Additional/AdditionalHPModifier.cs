namespace Server.Models.Modifiers.Additional
{
    public class AdditionalHPModifier : Modifier
    {
        public override string Name { get; } = "Дополнительное здоровье.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
