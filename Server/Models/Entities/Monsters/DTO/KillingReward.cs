namespace Server.Models.Entities.Monsters.DTO
{
    public class KillingReward
    {
        public string KilledMonsterName { get; set; }
        public int AddedExp { get; set; }

        public KillingReward(string killedMonsterName, int addedExp)
        {
            KilledMonsterName = killedMonsterName;
            AddedExp = addedExp;
        }
    }
}
