namespace Server.Models.Modifiers.Additional.Stats
{
    public class AddLuck : Modifier
    {
        public override string Name { get; } = "Дополнительная удача.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
