using Framework.Security2023.Cryptography;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Security2023.Services
{
    public static class InitialServices
    {
        public static void AddInitialServicesFra(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IServiceCryptography, ServiceCryptography>();
            serviceCollection.AddTransient<RespositoryUser>();
            serviceCollection.AddTransient<RepositoryUserLoginAttempts>();
            serviceCollection.AddTransient<IServiceRole, ServiceRole>();
            serviceCollection.AddTransient<RepositoryToken>();
            serviceCollection.AddTransient<IServiceToken, ServiceToken>();
        }
    }
}
