using Server.Hubs.Locations.BasePlaces;
using Server.Models.DTO;
using Server.Models.Interfaces;
using Server.Models.Skills;
using Server.Models.Spells;
using Server.Models.Utilities;

namespace Server.Models.Monsters
{
    public abstract class Monster : Entity
    {
        protected readonly IServiceFactory<UserStorage> userStorageFactory;
        public abstract int Id { get; set; }
        //public override VitalityHandler Vitality { get; protected set; }
        public int Exp { get; set; } = 0;
        public string ImagePath { get; set; } = string.Empty;
        public abstract string Target { get; protected set; }
        public abstract string PlaceName { get; protected set; }
        public abstract BattlePlace PlaceInstance { get; protected set; }
        public abstract double MinTimeAttack {  get; set; }
        public abstract double MaxTimeAttack { get; set; }

        public Monster(IServiceFactory<UserStorage> _userStorageFactory)
        {
            userStorageFactory = _userStorageFactory;
        }

        public abstract void SetTarget(string name);

        public MonsterDTO ToJson()
        {
            return new()
            {
                Id = Id,
                CurrentHP = Vitality.CurrentHP,
                MaxHP = Vitality.MaxHP,
                ImagePath = ImagePath,
                Name = Name,
                Target = Target,
                States = States.Keys.Select(x => x.ToJson()).ToList()
            };
        }

        public override void RemoveState<T>() where T : class
        {
            base.RemoveState<T>();
            PlaceInstance?.UpdateMonsters();
        }

        public override ResultUseSpell TakeHealing(int healing)
        {
            Vitality.TakeHealing(healing);
            return new(healing, Vitality.CurrentHP, Vitality.MaxHP);
        }

        public override ResultUseSpell TakeDamage(int damage)
        {
            Vitality.TakeDamage(damage);

            if (Vitality.CurrentHP <= 0)
            {
                PlaceInstance.RemoveMonster(this);
            }

            PlaceInstance.UpdateMonsters();
            return new(damage, Vitality.CurrentHP, Vitality.MaxHP);
        }

        public override void UseSpell(SpellType type, params Entity[] target)
        {
            var skill = new SkillCollection().PickSkill(type);
            var user = target.First() as ActiveUser;

            if (skill != null && user != null && CanAttack)
            {
                skill.Use(this, target);

                /*if (resultUse != null)
                    _ = user.AddBattleLog(resultUse);*/
            }
        }

        protected bool CheckPlayerInPlace(UserStorage storage, string name) => storage.ActiveUsers.Any(x => x.Name == name && x.Place == PlaceName);

        protected void SendBattleLog(ActiveUser user)
        {
            if (Vitality.CurrentHP <= 0)
            {
                _ = user.AddBattleLog($"{user.Name} уничтожил {Name}.");
            }
            else
            {
                _ = user.AddBattleLog($"{user.Name} скрылся от {Name}.");
            }
        }
    }
}
