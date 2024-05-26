using Server.Models.Interfaces;
using Server.Models.Monsters.States;
using Server.Models.Utilities;

namespace Server.Models.Monsters
{
    public class Orc : Monster
    {
        public Orc(IServiceFactory<UserStorage> userStorageFactory, string place)
            : base(userStorageFactory)
        {
            CurrentHP = 97;
            MaxHP = 97;
            Name = "Orc " + GetRandomName();
            ImagePath = "orc_image.png";
        }

        public override int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Target { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override string Place { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override State State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override double MinTimeAttack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override double MaxTimeAttack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Name { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override BaseStatsHandler Stats { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public override int Attack()
        {
            return 5;
        }

        public override Task SetTarget(string name)
        {
            throw new NotImplementedException();
        }

        private string GetRandomName()
        {
            string[] array = ["Serega", "Alex", "Grigor", "Maxim", "Artem", "Anton"];

            return array[new Random().Next(0, array.Length)];
        }

        public override int TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public override void UseSkill(SpellType type, params Entity[] target)
        {
            throw new NotImplementedException();
        }
    }
}
