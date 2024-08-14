namespace Server.Models.Handlers.Vitality
{
    public abstract class VitalityHandler
    {
        public int CurrentHP { get; protected set; }
        public int MaxHP { get; set; }
        public int RegenRateHP { get; protected set; }

        public abstract void TakeDamage(int damage);
        public abstract void TakeHealing(int healing);
        public abstract void ResetValues();
    }
}
