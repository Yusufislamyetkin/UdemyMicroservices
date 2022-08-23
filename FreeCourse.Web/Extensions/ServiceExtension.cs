using FreeCourse.Web.Handler;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FreeCourse.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
            services.AddHttpClient<IIdentityService, IdentityService>();


            // Eğer bir catalogService' sine istek yapacaksan bu base adress üzerinden yapacaksın diye belirtiyoruz. Delegemiz ile de adresimize giderken
            // elimiz dolu giidyoruz. (Client ıd ve secretımız ile oluşturduğumuz token ile)
            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();



            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                // Bu base address IPhotoStockService servisinin her httpclient (http) isteğinde yer alacaktır.
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();




            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                // Bu base address IPhotoStockService servisinin her httpclient (http) isteğinde yer alacaktır.
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();



            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                // Bu base address IPhotoStockService servisinin her httpclient (http) isteğinde yer alacaktır.
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();



            // Userservice IoC olurken. Delegate araya girerek token bilgisi gönderecek. Buradakı url ise direkt olarak identity Serverr url'si. 
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);

            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


            // Userservice IoC olurken. Delegate araya girerek token bilgisi gönderecek. Buradakı url ise direkt olarak identity Serverr url'si. 
            services.AddHttpClient<IPaymentService, PaymentService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");

            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();




        }
    }
}
