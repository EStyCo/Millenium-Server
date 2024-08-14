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
    public class Goblin : Monster
    {
        public override BattlePlace PlaceInstance { get; protected set; }
        public override string Target { get; protected set; } = string.Empty;
        public override string PlaceName { get; protected set; } = string.Empty;
        public override bool CanAttack { get; set; } = true;
        public override Dictionary<State, CancellationTokenSource> States { get; protected set; } = new();
        public override double MinTimeAttack { get; set; } = 3.0;
        public override double MaxTimeAttack { get; set; } = 5.0;
        public override StatsHandler Stats { get; set; }
        public override VitalityHandler Vitality { get; protected set; }
        public override string Description { get ; set ; }
        public override Dictionary<ItemType, int> DroppedItems { get; }
        public override ModifiersHandler Modifiers { get; set; }

        public Goblin(IServiceFactory<UserStorage> _userStorageFactory,
                   BattlePlace place,
                   Func<Task> updatingAction) : base(_userStorageFactory, updatingAction)
        {
            Exp = 25;
            Name = "Goblin " + GetRandomName();
            ImagePath = "monsters/goblin.png";
            Description = "Мелкое чмо.";
            PlaceInstance = place;
            PlaceName = place.NamePlace;

            Modifiers = new ModifiersHandler();
            Stats = new MonsterStatsHandler(12, 7, 3, 5,5,5);
            Vitality = new MonsterVitalityHandler(64, 64);

            DroppedItems = new Dictionary<ItemType, int>()
            {
                {ItemType.Apple, 6},
                {ItemType.Spacesuit, 13},
                {ItemType.TitanSword, 16},
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
                UseSpell(SpellType.Simple, user);

                double delayInSeconds = new Random().NextDouble() * (MaxTimeAttack - MinTimeAttack) + MinTimeAttack;
                await Task.Delay((int)(delayInSeconds * 1000));
            }

            Target = string.Empty;
            SendBattleLog(user);
        }

        private string GetRandomName()
        {
            string[] array = { "Chopa", "Oleg", "Denny", "Said", "Danik", "Vanya" };

            return array[new Random().Next(0, array.Length)];
        }
    }
}
