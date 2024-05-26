using Server.Models.Interfaces;
using Server.Models.Monsters.States;
using Server.Models.Skills;
using Server.Models.Utilities;

namespace Server.Models.Monsters
{
    public class Goblin : Monster
    {
        public override int Id { get; set; }
        public override string Name { get; protected set; }
        public override string Target { get; protected set; } = string.Empty;
        public override string Place { get; protected set; } = string.Empty;
        public override State State { get; set; } = new DefaultState();
        public override double MinTimeAttack { get; set; } = 2.0;
        public override double MaxTimeAttack { get; set; } = 4.0;
        public override BaseStatsHandler Stats { get ; protected set ; }

        public Goblin(IServiceFactory<UserStorage> _userStorageFactory, string place)
            : base(_userStorageFactory)
        {
            CurrentHP = 64;
            MaxHP = 64;
            Exp = 25;
            Name = "Goblin " + GetRandomName();
            ImagePath = "goblin_image.png";

            Stats = new MonsterStatsHandler(12, 7, 3);

            Place = place;
        }


        public override int Attack()
        {
            if (State.CanAttack())
            {
                return new Random().Next(15, 26);
            }
            return 0;
        }

        public override async Task SetTarget(string name)
        {
            Target = name;

            var storage = userStorageFactory.Create();
            var user = storage.ActiveUsers.FirstOrDefault(x => x.Name == name);

            if (user == null) return;

            while (CheckPlayerInPlace(storage, name) && CurrentHP > 0 && Target != string.Empty)
            {
                UseSkill(SpellType.Simple, user);

                /*if (damage > 0) _ = user.AddBattleLog($"Вас ударил {Name} на {damage} урона");

                user.Vitality.TakeDamage(damage);*/

                double delayInSeconds = new Random().NextDouble() * (MaxTimeAttack - MinTimeAttack) + MinTimeAttack;
                await Task.Delay((int)(delayInSeconds * 1000));
            }

            Target = string.Empty;
            SendBattleLog(user);
        }

        private bool CheckPlayerInPlace(UserStorage storage, string name) => storage.ActiveUsers.Any(x => x.Name == name && x.Place == Place);

        private string GetRandomName()
        {
            string[] array = { "Chopa", "Oleg", "Denny", "Said", "Danik", "Vanya" };

            return array[new Random().Next(0, array.Length)];
        }

        public override int TakeDamage(int damage)
        {
            return CurrentHP -= damage;
        }

        private void SendBattleLog(ActiveUser user)
        {
            if (CurrentHP <= 0)
            {
                _ = user.AddBattleLog($"Вы убили {Name}.");
            }
            else
            {
                _ = user.AddBattleLog($"Вы скрылись от {Name}.");
            }
        }

        public override void UseSkill(SpellType type, params Entity[] target)
        {
            var skill = new SkillCollection().PickSkill(type);
            var user = target.First() as ActiveUser;

            if (skill != null && user != null)
            {
                var resultUse = skill.Use(this, target);

                if (resultUse != null)
                    _ = user.AddBattleLog($"Вас ударил {Name} на {resultUse.Item1} урона."); 
            }
        }
    }
}
