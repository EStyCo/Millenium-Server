namespace Server.Models.Modifiers.Additional.Stats
{
    public class AddMastery : Modifier
    {
        public override string Name { get; } = "Дополнительное мастерство.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
