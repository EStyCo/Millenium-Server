using Server.Hubs.Locations.BasePlaces;
using Server.Models.Handlers;
using Server.Models.Interfaces;
using Server.Models.Monsters.States;
using Server.Models.Spells;
using Server.Models.Utilities;

namespace Server.Models.Monsters
{
    public class Orc : Monster
    {
        public Orc(IServiceFactory<UserStorage> userStorageFactory, string place)
            : base(userStorageFactory)
        {

            Name = "Orc " + GetRandomName();
            ImagePath = "orc_image.png";
        }

        public override int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Target { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override double MinTimeAttack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override double MaxTimeAttack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Name { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override StatsHandler Stats { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override bool CanAttack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Dictionary<State, CancellationTokenSource> States { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override string PlaceName { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override BattlePlace PlaceInstance { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override VitalityHandler Vitality { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public override Task SetTarget(string name)
        {
            throw new NotImplementedException();
        }

        private string GetRandomName()
        {
            string[] array = ["Serega", "Alex", "Grigor", "Maxim", "Artem", "Anton"];

            return array[new Random().Next(0, array.Length)];
        }
    }
}
