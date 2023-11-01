using RoboWebApiService.Service;
using RoboWebApiService.Interface;

namespace RoboWebApi.Configurations
{
    internal static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRoboWebApiService, RoboApiWebService>();                                                  

            return services;
        }
    }
    
}
