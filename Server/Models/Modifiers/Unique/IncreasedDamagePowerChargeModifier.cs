namespace Server.Models.Modifiers.Unique
{
    public class IncreasedDamagePowerChargeModifier : Modifier
    {
        public override string Name { get; } = "Модификатор увеличения урона от способности Сильный удар.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";

    }
}
