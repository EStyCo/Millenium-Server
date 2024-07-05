using Server.Models.DTO;

namespace Server.Models.Spells.States
{
    public abstract class State
    {
        public virtual bool IsStoppingState { get; } = false;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string ImagePath { get; }
        public abstract int CurrentTime { get; set; }
        public abstract int MaxTime { get; set; }

        public Entity Entity { get; protected set; }
        public Entity User { get; protected set; }
        public CancellationTokenSource CTS { get; protected set; }

        protected State(Entity user, Entity entity, CancellationTokenSource _CTS)
        {
            User = user;
            Entity = entity;
            CTS = _CTS;
        }

        public abstract Task Enter();
        public abstract void Exit();
        public abstract void Refresh();

        public StateDTO ToJson()
        {
            return new(Name, Description, ImagePath, CurrentTime);
        }
    }
}
