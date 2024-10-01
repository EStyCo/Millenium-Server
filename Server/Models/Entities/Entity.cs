using Server.Models.Handlers.Vitality;
using Server.Models.Handlers.Stats;
using Server.Models.Spells;
using Server.Models.Spells.States;
using Server.Models.Utilities;
using Server.Models.Handlers;
using Server.Models.Modifiers.Default;
using Server.Models.Modifiers.Increased;

namespace Server.Models.Entities
{
    public abstract class Entity
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public abstract bool CanAttack { get; set; }
        public abstract Dictionary<State, CancellationTokenSource> States { get; protected set; }
        public abstract StatsHandler Stats { get; set; }
        public abstract ModifiersHandler Modifiers { get; set; }
        public abstract VitalityHandler Vitality { get; protected set; }
        public abstract void UseSpell(SpellType type, params Entity[] target);
        public abstract ResultUseSpell TakeDamage(int damage);
        public abstract ResultUseSpell TakeHealing(int healing);
        public abstract void UpdateStates();
        public abstract int GetWeaponDamage();

        private CancellationTokenSource? UpdateStateToken { get; set; }

        public void AddState<T>(Entity user) where T : State
        {
            var cts = new CancellationTokenSource();
            var instance = Activator.CreateInstance(typeof(T), user, this, cts) as State;

            if (instance == null) return;

            var a = States.Keys.FirstOrDefault(x => x.GetType() == instance.GetType());
            if (a != null)
            {
                a.Refresh();
                return;
            }

            States.Add(instance, cts);
            _ = instance.Enter();

            if (States.Count > 0 && UpdateStateToken == null)
            {
                UpdateStateToken = new CancellationTokenSource();
                _ = Task.Run(async () =>
                {
                    while (UpdateStateToken != null)
                    {
                        UpdateStates();
                        await Task.Delay(1000);
                    }
                }, UpdateStateToken.Token);
            }
        }

        public virtual void RemoveState<T>() where T : State
        {
            var state = States.FirstOrDefault(x => x.Key.GetType() == typeof(T)).Key;
            if (state == null) return;

            state.Exit();
            States.Remove(state);

            var otherStoppingStates = States.FirstOrDefault(x => x.Key.IsStoppingState == true).Key;
            if (otherStoppingStates == null) CanAttack = true;

            UpdateStates();

            if (UpdateStateToken != null && States.Count == 0)
            {
                UpdateStateToken.Cancel();
                UpdateStateToken = null;
            }
        }

        public string Leading()
        {
            return $"/i{ImagePath}/i /b{Name}/b /b[{Vitality.CurrentHP}/{Vitality.MaxHP}]/b";
        }

        public int GetDefaultDamage()
        {
            var s = Stats;
            if (s == null) return 0;

            int luckDmg = 0;
            if (s.Luck <= 10)
                luckDmg = s.Luck;
            else
                luckDmg = 10;
            var masteryDmg = s.Mastery / 100 * 20;
            double countDmg = s.Strength * 2 + s.Mastery * (s.Agility / 100)
                + masteryDmg + luckDmg;

            var clearDmg = (int)Math.Round(countDmg) + GetWeaponDamage();


            var a = Modifiers.Get<LowLimitDamage>()?.Value ?? 0;
            var b = Modifiers.Get<HighLimitDamage>()?.Value ?? 0;
            var lowDmg = clearDmg - (double)clearDmg / 100 * a;
            var highDmg = clearDmg + (double)clearDmg / 100 * b;

            var dmg = new Random().Next((int)Math.Round(lowDmg), (int)Math.Round(highDmg) + 1);

            var modifier = Modifiers.Modifiers.FirstOrDefault(x => x.GetType() == typeof(IncreasedDamageModifier));
            double additionalDamage = (dmg * modifier?.Value ?? 0) / 100;
            dmg += (int)Math.Round(additionalDamage);

            return dmg;
        }
    }
}