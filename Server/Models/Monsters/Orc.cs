using Server.Hubs.Locations.BasePlaces;
using Server.Models.Handlers;
using Server.Models.Handlers.Stats;
using Server.Models.Handlers.Vitality;
using Server.Models.Interfaces;
using Server.Models.Spells.States;
using Server.Models.Utilities;
using Server.Models.Utilities.Slots;

namespace Server.Models.Monsters
{
    public class Orc : Monster
    {
        public override BattlePlace PlaceInstance { get; protected set; }
        public override string Target { get; protected set; } = string.Empty;
        public override string PlaceName { get; protected set; } = string.Empty;
        public override bool CanAttack { get; set; } = true;
        public override Dictionary<State, CancellationTokenSource> States { get; protected set; } = new();
        public override double MinTimeAttack { get; set; } = 3.0;
        public override double MaxTimeAttack { get; set; } = 7.0;
        public override StatsHandler Stats { get;  set; }
        public override VitalityHandler Vitality { get; protected set; }
        public override string Description { get; set; }

        public override Dictionary<ItemType, int> DroppedItems { get; }
        public override ModifiersHandler Modifiers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private const int CRIT_CHANCE = 20;

        public Orc(IServiceFactory<UserStorage> _userStorageFactory, 
                   BattlePlace place,
                   Func<Task> updatingAction) : base(_userStorageFactory, updatingAction)
        {
            Exp = 65;
            Name = "Orc " + GetRandomName();
            ImagePath = "monsters/orc.png";
            PlaceInstance = place;
            Description = "Здоровый бугай, иногда может наподдать по башне.";
            PlaceName = place.NamePlace;

            Stats = new MonsterStatsHandler(18, 15, 3,5,5,5);
            Vitality = new MonsterVitalityHandler(138, 138);

            DroppedItems = new Dictionary<ItemType, int>()
            {
                {ItemType.Apple, 2},
                {ItemType.TitanArmor, 7},
                {ItemType.TitanSword, 10},
            };
        }

        public override async void SetTarget(string name)
        {
            Target = name;

            var storage = userStorageFactory.Create();
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == name);

            if (user == null) return;

            while (CheckPlayerInPlace(storage, name) && Vitality.CurrentHP > 0 && Target != string.Empty)
            {
                if (TryPowerCharge())
                    UseSpell(SpellType.PowerCharge, user);
                else
                    UseSpell(SpellType.Simple, user);

                double delayInSeconds = new Random().NextDouble() * (MaxTimeAttack - MinTimeAttack) + MinTimeAttack;
                await Task.Delay((int)(delayInSeconds * 1000));
            }

            Target = string.Empty;
            SendBattleLog(user);
        }

        private string GetRandomName()
        {
            string[] array = ["Serega", "Alex", "Grigor", "Maxim", "Artem", "Anton"];

            return array[new Random().Next(0, array.Length)];
        }

        private bool TryPowerCharge()
        {
            if (new Random().Next(0, 101) <= CRIT_CHANCE) return true;
            return false;
        }
    }
}
