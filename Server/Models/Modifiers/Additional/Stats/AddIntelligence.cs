namespace Server.Models.Modifiers.Additional.Stats
{
    public class AddIntelligence : Modifier
    {
        public override string Name { get; } = "Дополнительный интеллект.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
