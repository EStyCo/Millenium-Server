﻿using Server.Models.Utilities;

namespace Server.Models.Spells.Models
{
    public class Rest : Spell
    {
        public Rest()
        {
            SpellType = SpellType.Rest;
            Name = "Вдохновение";
            CoolDown = 90;
            Description = "Сокращает время восстановления ваших способностей";
            ImagePath = "rest.png";
        }

        public override void Use(Entity _user, params Entity[] target)
        {
            var user = _user as ActiveUser;
            if (user != null)
            {
                user.ResetAllSpells();

                string log =  $"{user.Name} восстановил все способности!";
                SendBattleLog(log, user);
            }
        }
    }
}
