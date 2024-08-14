namespace Server.Models.Modifiers
{
    public class ModifierDTO(Modifier modifier)
    {
        public string Name { get; } = modifier.Name;
        public int Value { get; } = modifier.Value;
        public string Description { get; } = modifier.Description;
    }
}
