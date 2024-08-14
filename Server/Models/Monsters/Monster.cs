using Server.Hubs.Locations.BasePlaces;
using Server.Models.Interfaces;
using Server.Models.Utilities;
using Server.Models.Spells;
using Server.Models.Monsters.DTO;
using Server.Models.Spells.States;
using Server.Models.Utilities.Slots;

namespace Server.Models.Monsters
{
    public abstract class Monster : Entity
    {
        protected readonly IServiceFactory<UserStorage> userStorageFactory;
        public Func<Task> UpdatingAction { get; }
        public int Id { get; set; }
        public int Exp { get; set; } = 0;
        public string ImagePath { get; set; } = string.Empty;
        public abstract string Description { get; set; }
        public abstract string Target { get; protected set; }
        public abstract string PlaceName { get; protected set; }
        public abstract BattlePlace PlaceInstance { get; protected set; }
        public abstract double MinTimeAttack { get; set; }
        public abstract double MaxTimeAttack { get; set; }

        public abstract Dictionary<ItemType, int> DroppedItems { get; }

        public Monster(IServiceFactory<UserStorage> _userStorageFactory, Func<Task> updatingAction)
        {
            userStorageFactory = _userStorageFactory;
            UpdatingAction = updatingAction;
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
        public DetailsMonsterDTO DetailsToJson()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                MaxHP = Vitality.MaxHP,
                Exp = Exp,
                ImagePath = ImagePath,
                MinTimeAttack = (int)MinTimeAttack,
                MaxTimeAttack = (int)MaxTimeAttack,
                Strength = Stats.Strength,
                Agility = Stats.Agility,
                Intelligence = Stats.Intelligence,
                States = States.Keys.Select(x => x.ToJson()).ToList()
            };
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

            UpdatingAction?.Invoke();
            return new(damage, Vitality.CurrentHP, Vitality.MaxHP);
        }

        public override void UseSpell(SpellType type, params Entity[] target)
        {
            if (CanAttack) SpellFactory.Get(type)?.Use(this, target);
        }

        protected bool CheckPlayerInPlace(UserStorage storage, string name)
        {
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == name && x.Place == PlaceName);
            if (user == null) return false;
            return !user.States.Keys.OfType<WeaknessState>().Any();
        }

        protected void SendBattleLog(ActiveUser user)
        {
            _ = user.AddBattleLog($"{user.Name} скрылся от {Name}.");
            /*if (Vitality.CurrentHP <= 0)
            {
                _ = user.AddBattleLog($"{user.Name} уничтожил {Name}.");
            }*/
        }

        public override void UpdateStates()
        {
            PlaceInstance?.UpdateListMonsters();
        }

        public List<ItemType> DropItemsOnDeath()
        {
            var list = new List<ItemType>();
            foreach (var item in DroppedItems)
                if (new Random().Next(0, item.Value) == 0)
                    list.Add(item.Key);
            return list;
        }
    }
}
