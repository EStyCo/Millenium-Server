namespace Server.Models.Modifiers.Default
{
    public class HighLimitDamage : Modifier
    {
        public override string Name { get; } = "Верхняя планка урона.";
        public override int Value { get; set; } = 15;
        public override int OriginalValue { get; set; } = 15;
        public override string Description { get; } = "";
    }
}
