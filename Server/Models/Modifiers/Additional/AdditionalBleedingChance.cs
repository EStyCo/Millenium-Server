namespace Server.Models.Modifiers.Additional
{
    public class AdditionalBleedingChance : Modifier
    {
        public override string Name { get; } = "Дополнительный шанс наложения кровотечения.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
