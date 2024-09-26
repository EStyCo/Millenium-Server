namespace Server.Models.Modifiers.Additional
{
    public class AdditionalAgility : Modifier
    {
        public override string Name { get; } = "Дополнительная ловкость.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
