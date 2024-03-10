using Server.Models.DTO;

namespace Server.Models.Skills
{
    public interface IAttack
    {
        int Attack(CharacterDTO character);
    }
}
