namespace Server.Models.Modifiers.Default
{
    public class LowLimitDamage : Modifier
    {
        public override string Name { get; } = "Нижняя планка урона.";
        public override int Value { get; set; } = 15;
        public override int OriginalValue { get; set; } = 15;
        public override string Description { get; } = "";
    }
}
