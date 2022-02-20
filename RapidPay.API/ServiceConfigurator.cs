using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Mapper;
using RapidPay.API.Domain.Services;
using RapidPay.API.Infrastructure.Context;
using RapidPay.API.Infrastructure.Repository;

namespace RapidPay.API
{
    public static class ServiceConfigurator
    {
        /// <summary>
        /// Activate services configuration for custom Controls
        /// </summary>
        /// <param name="services">Services collection from Startup</param>
        /// <param name="configuration">Configuration context from Startup</param>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.AddDbContextPool<RapidPayDBContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("RapidPay")));

            services.AddAutoMapper(typeof(CreditCardMapper));

            services.AddTransient<IRapidPayRepository, RapidPayRepository>();
            services.AddTransient<ICardManagementService, CardManagementService>();
            //services.AddSingleton<IFeeManagementService, FeeManagementService>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddSingleton<IAuthenticateService, AuthenticateService>();
        }
    }
}
