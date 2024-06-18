using Server.Models.DTO;

namespace Server.Models.Monsters.States
{
    public abstract class State
    {
        public virtual bool IsStoppingSpell { get; } = false;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string ImagePath { get; }
        public Entity Entity { get; protected set; }
        public Entity User { get; protected set; }
        public CancellationTokenSource CTS { get; protected set; }

        protected State(Entity user, Entity entity, CancellationTokenSource _CTS)
        {
            User = user;
            Entity = entity;
            CTS = _CTS;
        }

        public abstract void Enter();
        public abstract void Exit();

        public StateDTO ToJson()
        {
            return new(Name, Description, ImagePath);
        }
    }
}
