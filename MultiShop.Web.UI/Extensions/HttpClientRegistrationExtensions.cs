using MultiShop.Web.UI.Handlers;
using MultiShop.Web.UI.Handlers;
using MultiShop.Web.UI.Services.BasketServices;
using MultiShop.Web.UI.Services.CargoServices.CargoCompanyServices;
using MultiShop.Web.UI.Services.CargoServices.CargoCustomerServices;
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
using MultiShop.Web.UI.Services.DiscountServices;
using MultiShop.Web.UI.Services.ImageServices;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.MessageService;
using MultiShop.Web.UI.Services.OrderServices.OrderAddressServices;
using MultiShop.Web.UI.Services.OrderServices.OrderDetailServices;
using MultiShop.Web.UI.Services.OrderServices.OrderOderingServices;
using MultiShop.Web.UI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.Web.UI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.Web.UI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.Web.UI.Services.StatisticServices.UserStatisticServices;
using MultiShop.Web.UI.Services.UserIdentityServices;
using MultiShop.Web.UI.Settings;
using System.IO;

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
            RegisterCargoServices(services, values, values.Cargo.Path);
            RegisterMessageServices(services, values, values.Message.Path);
            RegisterStatisticServices(services, values);
            RegisterMiscellaneousServices(services, values);
        }

        private static void RegisterBasketServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<IBasketService, BasketService, ResourceOwnerPasswordTokenHandler>(services, values, path);
        }

        private static void RegisterStatisticServices(IServiceCollection services, ServiceApiSettings values)
        {
            RegisterHttpClient<ICatalogStatisticService, CatalogStatisticService, ResourceOwnerPasswordTokenHandler>(services, values, values.Catalog.Path);
            RegisterHttpClient<IMessageStatisticService, MessageStatisticService, ResourceOwnerPasswordTokenHandler>(services, values, values.Message.Path);
            RegisterHttpClient<IDiscountStatisticService, DiscountStatisticService, ResourceOwnerPasswordTokenHandler>(services, values, values.Discount.Path);
            RegisterHttpClient<IUserStatisticService, UserStatisticService, ResourceOwnerPasswordTokenHandler>(services, values, values.IdentityServerUrl);
        }

        private static void RegisterCargoServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<ICargoCompanyService, CargoCompanyService, ResourceOwnerPasswordTokenHandler>(services, values, path);
            RegisterHttpClient<ICargoCustomerService, CargoCustomerService, ResourceOwnerPasswordTokenHandler>(services, values, path);
        }

        private static void RegisterMessageServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<IMessageService, MessageService, ResourceOwnerPasswordTokenHandler>(services, values, path);
        }

        private static void RegisterOrderServices(IServiceCollection services, ServiceApiSettings values, string path)
        {
            RegisterHttpClient<IOrderOrderingService, OrderOrderingService, ResourceOwnerPasswordTokenHandler>(services, values, path);
            RegisterHttpClient<IOrderAddressService, OrderAddressService, ResourceOwnerPasswordTokenHandler>(services, values, path);
            RegisterHttpClient<IOrderDetailService, OrderDetailService, ResourceOwnerPasswordTokenHandler>(services, values, path);
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

            services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
            {
                opt.BaseAddress = new Uri(values.IdentityServerUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            RegisterHttpClient<IImageService, ImageService, ResourceOwnerPasswordTokenHandler>(services, values, values.Image.Path);

            RegisterHttpClient<IDiscountService, DiscountService, ResourceOwnerPasswordTokenHandler>(services,values, values.Discount.Path);
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

//        public static void RegisterHttpClients(this WebApplicationBuilder builder)
//        {
//            var serviceApiSettings = builder.Configuration
//                .GetSection("ServiceApiSettings")
//                .Get<ServiceApiSettings>();

//            var services = builder.Services;

//            RegisterHttpClientsByCategory(services, serviceApiSettings);
//            RegisterMiscellaneousServices(services, serviceApiSettings.IdentityServerUrl);
//        }

//        private static void RegisterHttpClientsByCategory(IServiceCollection services, ServiceApiSettings settings)
//        {
//            RegisterHttpClientsForBasketServices(services, settings);
//            RegisterHttpClientsForOrderServices(services, settings);
//            RegisterHttpClientsForCatalogServices(services, settings);
//            RegisterHttpClientsForCommentServices(services, settings);
//            RegisterHttpClientsForMessageServices(services, settings);
//        }

//        private static void RegisterHttpClientsForBasketServices(IServiceCollection services, ServiceApiSettings settings)
//        {
//            RegisterHttpClient<IBasketService, BasketService, ResourceOwnerPasswordTokenHandler>(services, settings, settings.Basket.Path);
//        }

//        private static void RegisterHttpClientsForOrderServices(IServiceCollection services, ServiceApiSettings settings)
//        {
//            RegisterHttpClient<IOrderOrderingService, OrderOrderingService, ResourceOwnerPasswordTokenHandler>(services, settings, settings.Order.Path);
//            RegisterHttpClient<IOrderAddressService, OrderAddressService, ResourceOwnerPasswordTokenHandler>(services, settings, settings.Order.Path);
//        }

//        private static void RegisterHttpClientsForCatalogServices(IServiceCollection services, ServiceApiSettings settings)
//        {
//            var catalogServices = new[]
//            {
//                (typeof(ICategoryService), typeof(CategoryService)),
//                (typeof(IProductService), typeof(ProductService)),
//                (typeof(ISpecialOfferService), typeof(SpecialOfferService)),
//                (typeof(IFeatureSliderService), typeof(FeatureSliderService)),
//                (typeof(IFeatureService), typeof(FeatureService)),
//                (typeof(IOfferDiscountService), typeof(OfferDiscountService)),
//                (typeof(IBrandService), typeof(BrandService)),
//                (typeof(IAboutService), typeof(AboutService)),
//                (typeof(IProductImageService), typeof(ProductImageService)),
//                (typeof(IProductDetailService), typeof(ProductDetailService)),
//                (typeof(IContactService), typeof(ContactService))
//            };

//            foreach (var (serviceInterface, serviceImplementation) in catalogServices)
//            {
//                RegisterHttpClient(services, serviceInterface, serviceImplementation, typeof(ClientCredentialTokenHandler), settings, settings.Catalog.Path);
//            }
//        }

//        private static void RegisterHttpClientsForCommentServices(IServiceCollection services, ServiceApiSettings settings)
//        {
//            RegisterHttpClient<ICommentService, CommentService, ClientCredentialTokenHandler>(services, settings, settings.Comment.Path);
//        }

//        private static void RegisterHttpClientsForMessageServices(IServiceCollection services, ServiceApiSettings settings)
//        {
//            RegisterHttpClient<IMessageService, MessageService, ResourceOwnerPasswordTokenHandler>(services, settings, settings.Message.Path);
//        }

//        private static void RegisterMiscellaneousServices(IServiceCollection services, string identityServerUrl)
//        {
//            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

//            services.AddHttpClient<IUserService, UserService>(opt =>
//            {
//                opt.BaseAddress = new Uri(identityServerUrl);
//            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
//        }

//        private static void RegisterHttpClient<TInterface, TImplementation, THandler>(IServiceCollection services, ServiceApiSettings settings, string basePath)
//            where TInterface : class
//            where TImplementation : class, TInterface
//            where THandler : DelegatingHandler
//        {
//            services.AddHttpClient<TInterface, TImplementation>(opt =>
//            {
//                opt.BaseAddress = new Uri($"{settings.OcelotUrl}/{basePath}");
//            }).AddHttpMessageHandler<THandler>();
//        }

//        private static void RegisterHttpClient(IServiceCollection services, Type serviceInterface, Type serviceImplementation, Type handlerType, ServiceApiSettings settings, string basePath)
//        {
//            var method = typeof(HttpClientFactoryServiceCollectionExtensions)
//                .GetMethods()
//                .First(m => m.Name == nameof(HttpClientFactoryServiceCollectionExtensions.AddHttpClient) &&
//                            m.GetGenericArguments().Length == 3);

//            var genericMethod = method.MakeGenericMethod(serviceInterface, serviceImplementation, handlerType);

//            genericMethod.Invoke(null, new object[] { services, new Action<HttpClient>((opt) => { opt.BaseAddress = new Uri($"{settings.OcelotUrl}/{basePath}"); }) });
//        }