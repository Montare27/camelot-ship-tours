namespace Persistence
{
    using Application.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CamelotDbContext>(options=>
                options.UseNpgsql(config.GetConnectionString("CamelotDb")));
            services.AddTransient<ICamelotDbContext, CamelotDbContext>();
            return services;
        }
    }
}
