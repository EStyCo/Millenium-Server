namespace Server.Models.Handlers
{
    public class MonsterVitalityHandler : VitalityHandler
    {
        public override int CurrentHP { get ; protected set ; }
        public override int MaxHP { get; set; }

        public MonsterVitalityHandler(int currentHP, int maxHP)
        {
            CurrentHP = currentHP;
            MaxHP = maxHP;
        }

        public override void TakeDamage(int damage)
        {
            int temp = CurrentHP - damage;

            if (temp < 0) CurrentHP = 0;
            else CurrentHP = temp;
        }

        public override void TakeHealing(int healing)
        {
            if (CurrentHP + healing > MaxHP)
            {
                CurrentHP = MaxHP;
            }
            else
            {
                CurrentHP += healing;
            }
        }
    }
}
