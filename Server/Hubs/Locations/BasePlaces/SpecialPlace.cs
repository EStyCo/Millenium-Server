using Microsoft.AspNetCore.SignalR;
using Server.Models.Monsters;
using Server.Models;
using Server.Hubs.DTO;
using Server.Models.Utilities;

namespace Server.Hubs.Locations.BasePlaces
{
    public class SpecialPlace : BasePlace
    {
        public override string NamePlace { get; } = "masturbation";
        public override bool CanAttackUser { get; } = true;
        public override Dictionary<string, ActiveUser> Users { get; protected set; } = new();

        public SpecialPlace(IHubContext<PlaceHub> hubContext) : base(hubContext)
        {

        }

        public override void AttackUser(ActiveUser user, ActiveUser target, SpellType type)
        {
            /*var monster = Monsters.FirstOrDefault(x => x.Id == dto.IdMonster);
            int addingExp = 0;

            if (monster != null)
            {
                user.UseSpell(dto.Type, monster);

                if (monster.Target != user.Name) monster.SetTarget(user.Name);
                if (monster.Vitality.CurrentHP <= 0)
                {
                    Monsters.Remove(monster);
                    addingExp = monster.Exp;
                    ResetTargetUser(user);
                }

                UpdateMonsters();
            }

            return addingExp;*/
        }
    }
}
