using Core.Interfaces;
using Ninject;

namespace Services
{
    public static class ServiceInitializer
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRefreshTokenService>().To<RefreshTokenService>();
        }
    }
}