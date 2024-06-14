using Microsoft.AspNetCore.SignalR;
using Server.Models.Interfaces;
using Server.Models.Utilities;

namespace Server.Models.Handlers
{
    public abstract class VitalityHandler 
    {
        public abstract int CurrentHP { get; protected set; }
        public abstract int MaxHP { get; set; }

        public abstract void TakeDamage(int damage);
        public abstract void TakeHealing(int healing);
    }
}
