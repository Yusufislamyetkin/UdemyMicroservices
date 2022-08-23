using FreeCourse.Shared.Services;
using FreeCourse.Web.Handler;
using FreeCourse.Web.Helper;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {




            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));


            services.AddHttpContextAccessor();
            services.AddAccessTokenManagement(); // ClientAccessTokenCacheye izin verir.
            services.AddSingleton<PhotoHelper>();
            services.AddScoped<ISharedIdentityService,SharedIdentityService>();


            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();

            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IClientCredentialTokenService,ClientCredentialTokenService>();
         
            services.AddHttpClient<IIdentityService, IdentityService>();






            // Eðer bir catalogService' sine istek yapacaksan bu base adress üzerinden yapacaksýn diye belirtiyoruz. Delegemiz ile de adresimize giderken
            // elimiz dolu giidyoruz. (Client ýd ve secretýmýz ile oluþturduðumuz token ile)
            services.AddHttpClient<ICatalogService,CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();



            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                // Bu base address IPhotoStockService servisinin her httpclient (http) isteðinde yer alacaktýr.
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}"); 
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();



            // Userservice IoC olurken. Delegate araya girerek token bilgisi gönderecek. Buradaký url ise direkt olarak identity Serverr url'si. 
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
              opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
               
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                (CookieAuthenticationDefaults.AuthenticationScheme, opts =>
                {
                    opts.LoginPath = "/Auth/SignIn";
                    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                    opts.SlidingExpiration = true;
                    opts.Cookie.Name = "udemywebcookie";
                });



            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
