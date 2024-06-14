namespace Server.Models.Spells
{
    public class ResultUseSpell
    {
        public int Count { get; }
        public int CurrentHP { get; }
        public int MaxHP { get; }

        public ResultUseSpell(int count, int currentHP, int maxHP)
        {
            Count = count;
            CurrentHP = currentHP;
            MaxHP = maxHP;
        }
    }
}
