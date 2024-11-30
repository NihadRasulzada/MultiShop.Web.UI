using MultiShop.Web.UI.Handlers;
using MultiShop.Web.UI.Services;
using MultiShop.Web.UI.Services.BasketServices;
using MultiShop.Web.UI.Services.CatalogServices.AboutServices;
using MultiShop.Web.UI.Services.CatalogServices.BrandServices;
using MultiShop.Web.UI.Services.CatalogServices.CategoryServices;
using MultiShop.Web.UI.Services.CatalogServices.ContactServices;
using MultiShop.Web.UI.Services.CatalogServices.FeatureServices;
using MultiShop.Web.UI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.Web.UI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.Web.UI.Services.CatalogServices.ProductDetailServices;
using MultiShop.Web.UI.Services.CatalogServices.ProductImageServices;
using MultiShop.Web.UI.Services.CatalogServices.ProductServices;
using MultiShop.Web.UI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.Web.UI.Services.CommentServices;
using MultiShop.Web.UI.Services.Concrete;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.OrderServices.OrderAddressServices;
using MultiShop.Web.UI.Services.OrderServices.OrderOderingServices;
using MultiShop.Web.UI.Settings;

namespace MultiShop.Web.UI.Extensions
{
    public static class HttpClientRegistrationExtensions
    {
        public static void RegisterHttpClients(this WebApplicationBuilder builder)
        {
            var values = builder.Configuration
                .GetSection("ServiceApiSettings")
                .Get<ServiceApiSettings>();
            var services = builder.Services;

            RegisterBasketServices(services, values, values.Basket.Path);
            RegisterOrderServices(services, values, values.Order.Path);
            RegisterCatalogServices(services, values, values.Catalog.Path);
            RegisterCommentServices(services, values, values.Comment.Path);
            RegisterMiscellaneousServices(services, values);
        }

        private static void RegisterBasketServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<IBasketService, BasketService, ResourceOwnerPasswordTokenHandler>(services, values, path);
        }

        private static void RegisterOrderServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<IOrderOrderingService, OrderOrderingService, ResourceOwnerPasswordTokenHandler>(services, values, path);
            RegisterHttpClient<IOrderAddressService, OrderAddressService, ResourceOwnerPasswordTokenHandler>(services, values, path);
        }

        private static void RegisterCatalogServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<ICategoryService, CategoryService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IProductService, ProductService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<ISpecialOfferService, SpecialOfferService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IFeatureSliderService, FeatureSliderService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IFeatureService, FeatureService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IOfferDiscountService, OfferDiscountService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IBrandService, BrandService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IAboutService, AboutService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IProductImageService, ProductImageService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IProductDetailService, ProductDetailService, ClientCredentialTokenHandler>(services, values, path);
            RegisterHttpClient<IContactService, ContactService, ClientCredentialTokenHandler>(services, values, path);
        }

        private static void RegisterCommentServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<ICommentService, CommentService, ClientCredentialTokenHandler>(services, values, path);
        }

        private static void RegisterMiscellaneousServices(IServiceCollection services, ServiceApiSettings values)
        {
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(values.IdentityServerUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }

        private static void RegisterHttpClient<TInterface, TImplementation, THandler>(IServiceCollection services, ServiceApiSettings settings, string basePath)
            where TInterface : class
            where TImplementation : class, TInterface
            where THandler : DelegatingHandler
        {
            services.AddHttpClient<TInterface, TImplementation>(opt =>
            {
                opt.BaseAddress = new Uri($"{settings.OcelotUrl}/{basePath}");
            }).AddHttpMessageHandler<THandler>();
        }
    }
}
