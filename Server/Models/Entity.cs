using Server.Models.Handlers.Vitality;
using Server.Models.Handlers.Stats;
using Server.Models.Spells;
using Server.Models.Spells.States;
using Server.Models.Utilities;
using Server.Models.Handlers;

namespace Server.Models
{
    public abstract class Entity
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; }
        public abstract bool CanAttack { get; set; }
        public abstract Dictionary<State, CancellationTokenSource> States { get; protected set; }
        public abstract StatsHandler Stats { get; set; }
        public abstract ModifiersHandler Modifiers { get; set; }
        public abstract VitalityHandler Vitality { get; protected set; }
        public abstract void UseSpell(SpellType type, params Entity[] target);
        public abstract ResultUseSpell TakeDamage(int damage);
        public abstract ResultUseSpell TakeHealing(int healing);
        public abstract void UpdateStates();

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
    }
}