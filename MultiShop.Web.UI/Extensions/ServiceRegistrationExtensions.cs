using MultiShop.Web.UI.Handlers;
using MultiShop.Web.UI.Services.Concrete;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Settings;

namespace MultiShop.Web.UI.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddScoped<ILoginService, LoginService>();
            services.AddHttpContextAccessor();
            services.AddAccessTokenManagement();

            services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
            services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));

            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>();

            services.AddHttpClient();
            services.AddControllersWithViews();
        }
    }
}
