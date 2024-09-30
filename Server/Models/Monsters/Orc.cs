using Server.Hubs;
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
        public override ModifiersHandler Modifiers { get; set; }
        private const int CRIT_CHANCE = 20;

        public Orc(
            IServiceFactory<UserStorage> _userStorageFactory, 
            BattlePlace place,
            Func<Task> updatingAction) : base(_userStorageFactory, updatingAction)
        {
            Exp = 65;
            Name = "Орк";
            ImagePath = "monsters/orc.png";
            PlaceInstance = place;
            Description = "Здоровый бугай, иногда может наподдать по башне.";
            PlaceName = place.NamePlace;

            Stats = new MonsterStatsHandler(18, 15, 3,5,15,5);
            Vitality = new MonsterVitalityHandler(138, 138);
            Modifiers = new ModifiersHandler();

            DroppedItems = new Dictionary<ItemType, int>()
            {
                {ItemType.Apple, 3},
                {ItemType.TitanArmor, 18},
                {ItemType.HeavyBelt, 9},
                {ItemType.TitanSword, 13},
            };
        }

        private bool TryPowerCharge()
        {
            if (new Random().Next(0, 101) <= CRIT_CHANCE) return true;
            return false;
        }

        protected override void ActionAttack(ActiveUser user)
        {
            if (TryPowerCharge())
                UseSpell(SpellType.PowerCharge, user);
            else
                UseSpell(SpellType.Simple, user);
        }
        public override int GetWeaponDamage()
        {
            return 25;
        }
    }
}
