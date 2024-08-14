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
    public class PizzaPiece : Monster
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

        public PizzaPiece(IServiceFactory<UserStorage> _userStorageFactory,
                   BattlePlace place,
                   Func<Task> updatingAction) : base(_userStorageFactory, updatingAction)
        {
            Exp = 25;
            Name = "Кусочек Пиццы";
            ImagePath = "monsters/pizza_piece.png";
            Description = "Хочеца кушац!?";
            PlaceInstance = place;
            PlaceName = place.NamePlace;

            Stats = new MonsterStatsHandler(15, 12, 8, 5, 5, 5);
            Vitality = new MonsterVitalityHandler(45, 45);

            DroppedItems = new Dictionary<ItemType, int>()
            {
                {ItemType.Stone, 5},
                {ItemType.LeatherArmor, 22},
                {ItemType.Spacesuit, 17},
                {ItemType.ChainBoots, 13},
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
                if (new Random().Next(0, 100) <= 33)
                    UseSpell(SpellType.LowHealing, user);
                else
                    UseSpell(SpellType.Simple, user);

                double delayInSeconds = new Random().NextDouble() * (MaxTimeAttack - MinTimeAttack) + MinTimeAttack;
                await Task.Delay((int)(delayInSeconds * 1000));
            }

            Target = string.Empty;
            SendBattleLog(user);
        }
    }
}
