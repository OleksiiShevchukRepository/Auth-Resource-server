using Core.Interfaces;
using Ninject;

namespace Services
{
    public static class ServiceInitializer
    {
        public static void RegisterMongoServices(IKernel kernel)
        {
            kernel.Bind<IEventService>().To<EventService>();
        }

        public static void RegisterSqlServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRefreshTokenService>().To<RefreshTokenService>();
        }
    }
}