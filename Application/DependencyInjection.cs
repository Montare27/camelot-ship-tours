namespace Application
{
    using global::Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Services.SessionService;
    using Services.WishList;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IWishListService, WishListService>();      
            services.AddTransient<ISessionService, SessionService>();
            
            return services;
        }
    }
}
