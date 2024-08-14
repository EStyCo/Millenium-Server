namespace Server.Models.Modifiers.Increased
{
    public class IncreasedHPModifier : Modifier
    {
        public override string Name { get; } = "Модификатор увеличения здоровья";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}