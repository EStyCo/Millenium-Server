using Server.Hubs.Locations.BasePlaces;
using Server.Models.Interfaces;
using Server.Models.Utilities;
using Server.Models.Spells;
using Server.Models.Spells.States;
using Server.Models.Utilities.Slots;
using Server.Hubs;
using Server.Models.Entities;
using Server.Models.Entities.Monsters.DTO;

namespace Server.Models.Entities.Monsters
{
    public abstract class Monster : Entity
    {
        protected readonly IServiceFactory<UserStorage> userStorageFactory;
        protected CancellationTokenSource AttackCTS;
        public Func<Task> UpdatingAction { get; }
        public int Id { get; set; }
        public int Exp { get; set; } = 0;
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

        protected abstract void ActionAttack(ActiveUser user);

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
            if (CanAttack)
                SpellFactory.Get(type)?.Use(this, target);
        }

        protected bool CheckPlayerInPlace(UserStorage storage, string name)
        {
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == name && x.Place == PlaceName);
            if (user == null) return false;
            return !user.States.Keys.OfType<WeaknessState>().Any();
        }

        protected void SendBattleLog(ActiveUser user)
        {
            if (Vitality.CurrentHP <= 0)
            {
                _ = user.AddBattleLog($"{user.Leading()} жестоко покарал {Leading()} . Получено /b{Exp}exp/b");
            }
            else
            {
                _ = user.AddBattleLog($"{user.Leading()} скрылся с глаз от {Leading()}");
            }
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

        protected void RefreshAttackToken()
        {
            if (AttackCTS != null)
                if (!AttackCTS.IsCancellationRequested)
                    AttackCTS?.Cancel();
            AttackCTS = new CancellationTokenSource();
        }

        public async Task SetTarget(string name)
        {
            RefreshAttackToken();

            var storage = userStorageFactory.Create();
            var user = storage.GetUser(name);
            if (user == null) return;
            Target = name;

            while (CheckPlayerInPlace(storage, name) && Vitality.CurrentHP > 0
                && !AttackCTS.IsCancellationRequested)
            {
                ActionAttack(user);
                await Task.Run(ActionDelay, AttackCTS.Token);
            }
            Target = string.Empty;
            SendBattleLog(user);
        }

        private async Task ActionDelay()
        {
            double generalTimeInSec = new Random().NextDouble() * (MaxTimeAttack - MinTimeAttack) + MinTimeAttack;
            double currentTimeInSec = 0;

            while (currentTimeInSec <= generalTimeInSec && !AttackCTS.IsCancellationRequested)
            {
                await Task.Delay(100);
                currentTimeInSec += 0.1;
            }
        }

        #region Json Methods
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
                States = States.Keys.Select(x => x.ToJson()).ToList(),
            };
        }

        public DetailsMonsterDTO DetailsToJson()
        {
            var rewards1 = ItemFactory.GetList(DroppedItems.Keys.ToList());
            var rewards2 = rewards1.Select(x => x.ToJson()).ToList();
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
                States = States.Keys.Select(x => x.ToJson()).ToList(),
                Rewards = rewards2
            };
        }
        #endregion
    }
}
