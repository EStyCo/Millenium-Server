using Server.Models.Handlers;
using Server.Models.Monsters.States;
using Server.Models.Spells;
using Server.Models.Utilities;

namespace Server.Models
{
    public abstract class Entity
    {
        public abstract string Name { get; protected set; }
        public abstract bool CanAttack { get; set; }
        public abstract Dictionary<State, CancellationTokenSource> States { get; protected set; }
        public abstract StatsHandler Stats { get; protected set; }
        public abstract VitalityHandler Vitality { get; protected set; }
        public abstract void UseSpell(SpellType type, params Entity[] target);
        public abstract ResultUseSpell TakeDamage(int damage);
        public abstract ResultUseSpell TakeHealing(int healing);

        public void AddState<T>(Entity user) where T : State
        {
            var cts = new CancellationTokenSource();
            var instance = Activator.CreateInstance(typeof(T), user, this, cts) as State;

            if (instance == null) return;
            States.TryAdd(instance, cts);
        }

        public virtual void RemoveState<T>() where T : State
        {
            var state = States.FirstOrDefault(x => x.Key.GetType() == typeof(T)).Key;
            if (state == null) return;

            state.CTS.Cancel();
            States.Remove(state);
            state.Exit();

            var otherStoppingStates = States.FirstOrDefault(x => x.Key.IsStoppingSpell == true).Key;
            if (otherStoppingStates == null) CanAttack = true;
        }
    }
}



/*var state = States.FirstOrDefault(x => x.Key.GetType() == typeof(T)).Key;

            if (state == null) return;

            if (!state.CTS.IsCancellationRequested)
            {
                state.CTS.Cancel();
                return;
            }
            else
            {
                States.Remove(state);
                state.Exit();
            }

            var otherStoppingStates = States.FirstOrDefault(x => x.Key.IsStoppingSpell == true).Key;
            if (otherStoppingStates == null) CanAttack = true;*/