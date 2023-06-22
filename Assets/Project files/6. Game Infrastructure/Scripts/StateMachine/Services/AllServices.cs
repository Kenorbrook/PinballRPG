public class AllServices
{
    private static AllServices _instance;
    public static AllServices Container => _instance ??= new AllServices();


    public TService GetSingle<TService>() where TService :class, IService
    {
        return Implementation<TService>.ServiceInstance;
    }

    public void RegisterServiceAsSingle<TService>(TService levelFactory) where TService : class, IService
    {
        Implementation<TService>.ServiceInstance = levelFactory;
    }

    private static class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}