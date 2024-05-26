using Server.Models.Interfaces;
using Server.Models.Monsters.States;

namespace Server.Models.Monsters
{
    public abstract class Monster : Entity
    {
        protected readonly IServiceFactory<UserStorage> userStorageFactory;

        public abstract int Id { get; set; }
        public int CurrentHP { get; set; } = 0;
        public int MaxHP { get; set; } = 0;
        public int Exp { get; set; } = 0;
        public string ImagePath { get; set; } = string.Empty;
        public abstract string Target { get; protected set; }
        public abstract string Place { get; protected set; }
        public abstract State State { get; set; }
        public abstract double MinTimeAttack {  get; set; }
        public abstract double MaxTimeAttack { get; set; }

        protected Monster(IServiceFactory<UserStorage> _userStorageFactory)
        {
            userStorageFactory = _userStorageFactory;
        }

        public abstract int Attack();
        public abstract Task SetTarget(string name);
        //public abstract int TakeDamage(int damage);
    }
}
