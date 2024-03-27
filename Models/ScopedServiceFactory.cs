using Server.Models.Interfaces;

namespace Server.Models
{
    public class ScopedServiceFactory<T> : IServiceFactory<T>
    {
        private readonly IServiceScopeFactory scopeFactory;

        public ScopedServiceFactory(IServiceScopeFactory _scopeFactory)
        {
            scopeFactory = _scopeFactory;
        }

        public T Create()
        {
            return scopeFactory.CreateScope().ServiceProvider.GetRequiredService<T>();
        }
    }
}
