namespace Server.Models.Modifiers.Unique
{
    public class LuckyBleedingChance : Modifier
    {
        public override string Name { get; } = "Шанс наложения кровотечения удачлив";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
