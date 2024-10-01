using Server.Models.Entities;

namespace Server.Models.Handlers
{
    public interface IHandler
    {
        public ActiveUser User { get; }
    }
}
