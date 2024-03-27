namespace Server.Models.Interfaces
{
    public interface IServiceFactory<out T>
    {
        T Create();
    }
}
