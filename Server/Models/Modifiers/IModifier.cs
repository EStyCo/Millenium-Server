namespace Server.Models.Modifiers
{
    public abstract class Modifier
    {
        public abstract string Name { get; }
        public abstract int Value { get; set; }
        public abstract int OriginalValue { get; set; }
        public abstract string Description { get; }

        public void Reset()
        {
            Value = OriginalValue;
        }
        public ModifierDTO ToJson()
        {
            return new ModifierDTO(this);
        }
    }
}
